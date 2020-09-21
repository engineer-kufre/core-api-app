using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.DTOs
{
    public class ReturnedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
