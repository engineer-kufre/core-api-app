using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiApp.DTOs;
using CoreApiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        //[HttpGet]
        //public IActionResult GetAllLoggedInUsers()
        //{

        //}

        [HttpGet]
        public IActionResult GetAllRegisteredUsers()
        {
            //get all users from AspNetUsers table
            var users = _userManager.Users;

            var result = new List<ReturnedUser>();

            //create each DTO object for each user and pass into result list
            foreach (var user in users)
            {
                var returnedUser = new ReturnedUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedOn = user.CreatedOn
                };
                result.Add(returnedUser);
            }
            
            return Ok(result);
        }
    }
}
