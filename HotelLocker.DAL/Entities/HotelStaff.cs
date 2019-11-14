using HotelLocker.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace HotelLocker.DAL.Entities
{
    public class HotelStaff : User
    {
        public HotelStaff() : base() { }



        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public int? HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public StaffCategory Category { get; set; }
    }
}
