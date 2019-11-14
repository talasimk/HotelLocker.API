using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.Exceptions
{
    public class DBException : CustomException
    {
        public DBException(string message) : base(message) { }
    }
}
