using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelLocker.DAL.Entities
{
    public class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
            HotelStaffs = new HashSet<HotelStaff>();
            UserBlackLists = new HashSet<UserBlackList>();
        }

        public int Id { get; set; }
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
        
        public int Stars { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<HotelStaff> HotelStaffs { get; set; }
        public virtual ICollection<UserBlackList> UserBlackLists { get; set; }
        public HotelAdmin HotelAdmin { get; set; }
        public int? HotelAdminId { get; set; }
    }
}
