using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.Validators
{
    public static class ModelValidator
    {
        public static ValidationResults IsValid(object obj)
        {
            if (obj == null)
                return new ValidationResults()
                {
                    Successed = false,
                    ValidationResultsMessages = new List<ValidationResult>()
                    {
                        new ValidationResult("Empty values")
                    }
                };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            bool succesed = Validator.TryValidateObject(obj, context, results, true);
            return new ValidationResults()
            {
                Successed = succesed,
                ValidationResultsMessages = results
            };
        }
    }
}
