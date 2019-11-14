using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.DTOs
{
    public class ConfirmEmailDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
