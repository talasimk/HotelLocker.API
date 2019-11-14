using HotelLocker.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.DAL.Entities
{
    public class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomAccesses = new HashSet<RoomAccess>();
        }

        public int Id { get; set; }

        public int NumberInHotel { get; set; }

        [Required]
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public int Beds { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public RoomCategory Category { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomAccess> RoomAccesses { get; set; }
    }
}
