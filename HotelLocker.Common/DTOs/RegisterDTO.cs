using HotelLocker.Common.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs
{
    public class RegistrationDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string Passport { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailValidator]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
