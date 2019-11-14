using HotelLocker.Common.Enums;
using HotelLocker.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace HotelLocker.Common.DTOs
{
    public class RegisterStaffDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public StaffCategory Category { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailValidator]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
