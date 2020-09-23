using System;

namespace CoreApiApp.DTOs
{
    //model for a Returned User Data Transfer Object
    public class ReturnedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
