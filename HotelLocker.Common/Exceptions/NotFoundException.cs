using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
