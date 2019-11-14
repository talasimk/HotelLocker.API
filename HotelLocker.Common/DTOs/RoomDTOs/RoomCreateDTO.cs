using HotelLocker.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.RoomDTOs
{
    public class RoomCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public int NumberInHotel { get; set; }

        [Required]
        public int HotelId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public int Beds { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [Range(1, 13)]
        public RoomCategory Category { get; set; }
    }
}
