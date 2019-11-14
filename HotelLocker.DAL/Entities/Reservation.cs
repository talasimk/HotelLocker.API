using System;

namespace HotelLocker.DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
