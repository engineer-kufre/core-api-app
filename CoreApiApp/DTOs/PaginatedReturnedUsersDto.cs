using System.Collections.Generic;

namespace CoreApiApp.DTOs
{
    public class PaginatedReturnedUsersDto
    {
        //model for a Paginated List of Returned User Data Transfer Objects
        public string CurrentPage { get; set; }
        public List<ReturnedUser> ReturnedUsers { get; set; }
    }
}
