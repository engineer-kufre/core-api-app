using System;
using System.Collections.Generic;

namespace CoreApiApp.Services
{
    //model for objects bearing userManager operation responses
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
    }
}
