using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Mappers;
using HotelLocker.Common.DTOs.ReservationDTOs;
using HotelLocker.Common.Exceptions;
using HotelLocker.Common.Validators;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork context;

        public ReservationService(IUnitOfWork context)
        {
            this.context = context;
        }

        public async Task<ReservationDTO> CreateReservation(ReservationCreateDTO reservationCreateDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(reservationCreateDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var room = context.Rooms.Get(reservationCreateDTO.RoomId);
            if (room == null)
                throw new NotFoundException("No such room");

            Guest guest = context.Guests.Get(userId);
            if(context.UserBlackLists.GetAll().Where(x => x.GuestId == userId && x.HotelId == room.HotelId).Count()!=0)
                throw new ValidationException("You are in a hotel's black list");

            if (reservationCreateDTO.EndDate < reservationCreateDTO.StartDate)
                throw new ValidationException("Reservation end date can't be greater than a start date");

            if (reservationCreateDTO.EndDate - reservationCreateDTO.StartDate > TimeSpan.FromDays(20))
                throw new ValidationException("Reservation duration can't be greater than a 20 days");

            bool canBeReserved =  room.Reservations
                .Where(r =>  r.EndDate < reservationCreateDTO.StartDate || r.StartDate > reservationCreateDTO.EndDate)
                .Count() == room.Reservations.Count();
            if (!canBeReserved)
            {
                throw new ValidationException("Room is not available is this time");
            }

            var reservation = reservationCreateDTO.ToReservation();
            reservation.GuestId = userId;
            context.Reservations.Create(reservation);
            await context.SaveAsync();
            return reservation.ToReservationDTO();
        }

        public async Task<ReservationDTO> EditReservation(ReservationEditDTO reservationEditDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(reservationEditDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var reservation = context.Reservations.Get(reservationEditDTO.Id);
            if (reservation == null)
                throw new NotFoundException("No such reservation");

            if (reservation.GuestId != userId)
                throw new PermissionException();
            if (reservationEditDTO.StartDate != null && reservationEditDTO.EndDate != null)
            {
                bool canBeReserved = context.Reservations.GetAll()
                    .Where(r => r.RoomId == reservation.RoomId && r.Id != reservation.Id && (r.StartDate < reservationEditDTO.StartDate && r.EndDate > reservationEditDTO.StartDate) || (r.StartDate < reservationEditDTO.EndDate && r.EndDate > reservationEditDTO.EndDate))
                    .Count() == 0;
                if (!canBeReserved)
                {
                    throw new ValidationException("Room is not available is this time");
                }
                reservation.StartDate = reservationEditDTO.StartDate;
                reservation.EndDate = reservationEditDTO.EndDate;

            }
            if(!string.IsNullOrEmpty(reservationEditDTO.AdditionalInfo))
            {
                reservation.AdditionalInfo = reservationEditDTO.AdditionalInfo;
            }
            context.Reservations.Update(reservation);
            await context.SaveAsync();
            return reservation.ToReservationDTO();
        }

        public async Task ConfirmPayment(int reservationId, int userId)
        {
            var reservation = context.Reservations.Get(reservationId);
            if (reservation == null)
                throw new NotFoundException("No such reservation");

            if (reservation.Room.Hotel.HotelAdminId != userId)
                throw new PermissionException();

            reservation.IsPaid = true;
            context.Reservations.Update(reservation);
            await context.SaveAsync();
        }

        public async Task DeleteReservation(int id, int userId)
        {
            var reservation = context.Reservations.Get(id);
            if (reservation == null)
                throw new NotFoundException("No such reservation");

            if (reservation.GuestId != userId)
                throw new PermissionException();
            context.Reservations.Delete(reservation.Id);
            await context.SaveAsync();
        }

         public List<ReservationDTO> GetHotelReservations(int adminId, DateTime from, DateTime to, int roomId, int guestId)
        {
            HotelAdmin hotelAdmin = context.HotelAdmins.Get(adminId);
            int hotelId = hotelAdmin.Hotel.Id;
            var reservations = context.Reservations.GetAll()
                .Where(r => r.Room.HotelId == hotelId);
            if (from != null) reservations = reservations.Where(r => r.StartDate >= from);
            if (to != null && to > from) reservations = reservations.Where(r => r.EndDate <= to);
            if(roomId != 0) reservations = reservations.Where(r => r.RoomId == roomId);
            if (guestId != 0) reservations = reservations.Where(r => r.GuestId == guestId);
            return reservations
                .Select(r => r.ToReservationDTO())
                .ToList();
        }

        public List<ReservationDTO> GetUserReservationsHistory(int userId, DateTime from, DateTime to, int hotelId)
        {
            Guest hotelAdmin = context.Guests.Get(userId);
            var reservations = context.Reservations.GetAll()
                .Where(r => r.GuestId == userId);
            if (from != null) reservations = reservations.Where(r => r.StartDate >= from);
            if (to != null && to > from) reservations = reservations.Where(r => r.EndDate <= to);
            if (hotelId != 0) reservations = reservations.Where(r => r.Room.HotelId == hotelId);
            return reservations
                .Select(r => r.ToReservationDTO())
                .ToList();
        }
    }
}
