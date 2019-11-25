using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.ReservationDTOs
{
    public class ReservationCreateDTO
    {
        [Required]

        public int RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [MaxLength(200)]
        public string AdditionalInfo { get; set; }
    }
}
