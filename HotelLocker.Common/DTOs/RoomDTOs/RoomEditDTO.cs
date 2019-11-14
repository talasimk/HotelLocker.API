using HotelLocker.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.RoomDTOs
{
    public class RoomEditDTO
    {
        [Required]
        public int Id { get; set; }

        public int NumberInHotel { get; set; }


        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }


        [Range(1, 10)]
        public int Beds { get; set; }


        [Range(1, 13)]
        public RoomCategory Category { get; set; }
    }
}
