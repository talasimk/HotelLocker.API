using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.DTOs
{
    public class AccessByWorkerDTO
    {
        public int RoomId { get; set; }
        public string Reason { get; set; }
    }
}
