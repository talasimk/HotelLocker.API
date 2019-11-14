using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
