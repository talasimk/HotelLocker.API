using HotelLocker.Common.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.HotelsDTO
{
    public class HotelCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [Range(1, 6)]
        public int Stars { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailValidator]
        public string AdminEmail { get; set; }
    }
}
