using System.ComponentModel.DataAnnotations;
using ChildGrowth.Domain.Enum;

namespace ChildGrowth.API.Payload.Request.User
{
    public class UpdateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid international phone number.")]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UpdateUserRequest), nameof(ValidateDateOfBirth))]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
        public string Gender { get; set; } = null!;

        public static ValidationResult? ValidateDateOfBirth(DateOnly dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}