using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.HotelsDTO
{
    public class HotelDTO
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]

        public string Address { get; set; }
        [MaxLength(50)]

        public string Country { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [Range(1, 6)]

        public int Stars { get; set; }
    }
}
