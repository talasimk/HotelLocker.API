using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelLocker.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            RoomAccesses = new HashSet<RoomAccess>();
        }
        public virtual ICollection<RoomAccess> RoomAccesses { get; set; }
    }
}
