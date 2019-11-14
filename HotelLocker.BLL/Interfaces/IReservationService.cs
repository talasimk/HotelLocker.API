using HotelLocker.Common.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDTO> CreateReservation(ReservationCreateDTO reservationCreateDTO, int userId);
        List<ReservationDTO> GetHotelReservations(int adminId, DateTime from, DateTime to, int roomId, int guestId);
        List<ReservationDTO> GetUserReservationsHistory(int userId, DateTime from, DateTime to, int hotelId);
        Task DeleteReservation(int id, int userId);
        Task<ReservationDTO> EditReservation(ReservationEditDTO reservationEditDTO, int userId);
        Task ConfirmPayment(int reservationId, int userId);
    }
}
