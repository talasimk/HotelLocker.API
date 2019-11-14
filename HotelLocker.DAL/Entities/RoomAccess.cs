using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.DAL.Entities
{
    public class RoomAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsOpen { get; set; }

    }
}
