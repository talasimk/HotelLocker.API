using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.DTOs.UserBlackListDTOs
{
    public class UserBlackListDTO
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Reason { get; set; }
    }
}
