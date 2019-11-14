using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.DAL.Entities
{
    public class Guest : User
    {
        public Guest() : base()
        {
            Reservations = new HashSet<Reservation>();
            UserBlackLists = new HashSet<UserBlackList>();
        }


        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Passport { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserBlackList> UserBlackLists { get; set; }
    }
}
