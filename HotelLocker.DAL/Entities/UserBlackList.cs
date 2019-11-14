using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.DAL.Entities
{
    public class UserBlackList
    {
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public string Reason { get; set; }
    }
}
