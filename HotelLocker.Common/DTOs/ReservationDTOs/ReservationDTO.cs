using HotelLocker.Common.DTOs.HotelsDTO;
using HotelLocker.Common.DTOs.RoomDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.DTOs.ReservationDTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public string GuestEmail { get; set; }
        public string GuestName { get; set; }
        public RoomDTO Room { get; set; }
        public HotelDTO Hotel { get; set; }
        public decimal Sum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
