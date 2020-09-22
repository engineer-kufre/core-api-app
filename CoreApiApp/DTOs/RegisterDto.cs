using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Entry is longer than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Entry is longer than 30 characters")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Entry is longer than 30 characters")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(17, ErrorMessage = "Entry is longer than 17 characters")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
