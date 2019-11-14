using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.Validators
{
    public class ValidationResults
    {
        public List<ValidationResult> ValidationResultsMessages { get; set; }
        public bool Successed { get; set; }

    }
}
