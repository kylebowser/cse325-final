using System.ComponentModel.DataAnnotations;

namespace BreadOfLife.Models
{
    public class BakerRegistrationModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be a valid 5-digit US ZIP code.")]
        public string ZipCode { get; set; }
    }

    public class BakerLoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}