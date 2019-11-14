using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs.ReservationDTOs
{
    public class ReservationEditDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
       
        [MaxLength(300)]
        public string AdditionalInfo { get; set; }
    }
}
