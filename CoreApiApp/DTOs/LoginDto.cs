using System.ComponentModel.DataAnnotations;

namespace CoreApiApp.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }
    }
}