using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HotelLocker.Common.Validators
{
    public class EmailValidatorAttribute : ValidationAttribute
    {
        private readonly string pattern;

        public EmailValidatorAttribute()
        {
            pattern = @"^(?!.*?\.\.)[a-zA-Z0-9+-]+[\w-\.\+]+[a-zA-Z0-9+-]@(([a-zA-Z0-9]*[a-zA-Z0-9-]+[a-zA-Z0-9]{1,})+\.)+((?=.*[a-zA-Z])[a-zA-Z0-9]*[a-zA-Z0-9-]+[a-zA-Z0-9]{1,})$";
        }
        public override bool IsValid(object obj)
        {
            string email = obj as string;
            if (!string.IsNullOrEmpty(email))
            {
                return Regex.IsMatch(email, pattern);
            }
            return false;
        }
    }
}
