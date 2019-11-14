using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.Common.Exceptions
{
    public class PermissionException : CustomException
    {
        public PermissionException() : base("You don't have a permission") { }
    }
}
