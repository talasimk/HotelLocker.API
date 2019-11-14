using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.UserBlackListDTOs
{
    public class UserBLEditDTO
    {
        [Required]
        public int GuestId { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
