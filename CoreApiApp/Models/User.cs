using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.Models
{
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
