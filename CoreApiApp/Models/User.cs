using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiApp.Models
{
    // model class for users extends IdentityUser class with required user properties not available in Identity
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Entry is longer than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Entry is longer than 30 characters")]
        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
