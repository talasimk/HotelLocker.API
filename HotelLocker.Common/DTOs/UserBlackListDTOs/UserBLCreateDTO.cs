using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.UserBlackListDTOs
{
    public class UserBLCreateDTO
    {
        [Required]
        public int GuestId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }
    }
}
