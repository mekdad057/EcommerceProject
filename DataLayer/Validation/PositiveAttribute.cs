using System.ComponentModel.DataAnnotations;

namespace DataLayer.Validation
{
    public class PositiveAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Price is required");
            }

            if (value is decimal price && price > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Price must be a positive value.");
        }
    }
}
