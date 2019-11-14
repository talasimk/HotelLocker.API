using System;

namespace HotelLocker.Common.DTOs
{
    public class RoomAccessDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsOpen { get; set; }
        public string Reason { get; set; }
    }
}
