using HotelLocker.Common.DTOs.ReservationDTOs;
using HotelLocker.DAL.Entities;

namespace HotelLocker.BLL.Mappers
{
    public static class ReservationMapperExtension
    {
        public static Reservation ToReservation(this ReservationCreateDTO dto)
        {
            return new Reservation()
            {
                RoomId = dto.RoomId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                AdditionalInfo = dto.AdditionalInfo
            };
        }

        public static ReservationDTO ToReservationDTO(this Reservation reservation)
        {
            var date = reservation.EndDate - reservation.StartDate;
            return new ReservationDTO()
            {
                Room = reservation.Room.ToRoomDTO(),
                Hotel = reservation.Room.Hotel.ToHotelDTO(),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                AdditionalInfo = reservation.AdditionalInfo,
                Sum = reservation.Room.Price * date.Days,
                IsPaid = reservation.IsPaid,
                Id = reservation.Id,
                GuestEmail = reservation.Guest.Email,
                GuestName = reservation.Guest.FirstName + " " + reservation.Guest.LastName
            };
        }
    }
}
