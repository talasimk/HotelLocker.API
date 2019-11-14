using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelLocker.Common.Validators
{
    public static class ValidationExceptionBuilder
    {
        public static ValidationException BuildValidationException(ValidationResults validationResults)
        {
            var errorMessages = validationResults.ValidationResultsMessages;
            StringBuilder summaryErrorMessage = new StringBuilder();
            foreach (ValidationResult result in errorMessages)
            {
                summaryErrorMessage.Append(result.ErrorMessage);
            }
            return new ValidationException(summaryErrorMessage.ToString());
        }

    }
}
