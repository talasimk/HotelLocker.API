﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.DTOs
{
    public class AccessByWorkerDTO
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
