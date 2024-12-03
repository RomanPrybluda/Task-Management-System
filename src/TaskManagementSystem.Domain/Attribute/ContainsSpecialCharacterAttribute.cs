using System.ComponentModel.DataAnnotations;

namespace TMS.Domain.Attribute
{
    public class ContainsSpecialCharacterAttribute : ValidationAttribute
    {
        private readonly char[] specialCharacters = "!@#$%^&*()_-+=<>?/{}~|".ToCharArray();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("The field is required.");
            }

            var stringValue = value as string;
            if (stringValue == null)
            {
                return new ValidationResult("The field must be a string.");
            }

            if (!stringValue.Any(ch => specialCharacters.Contains(ch)))
            {
                return new ValidationResult("The field must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}
