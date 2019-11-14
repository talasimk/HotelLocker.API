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

            if (reservationCreateDTO.EndDate < reservationCreateDTO.StartDate)
                throw new ValidationException("Reservation end date can't be greater than a start date");

            if (reservationCreateDTO.EndDate - reservationCreateDTO.StartDate > TimeSpan.FromDays(20))
                throw new ValidationException("Reservation duration can't be greater than a 20 days");

            bool canBeReserved =  room.Reservations
                .Where(r => r.EndDate < reservationCreateDTO.StartDate || r.StartDate > reservationCreateDTO.EndDate)
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
