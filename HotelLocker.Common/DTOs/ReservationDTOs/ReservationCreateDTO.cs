using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.DTOs.ReservationDTOs
{
    public class ReservationCreateDTO
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
