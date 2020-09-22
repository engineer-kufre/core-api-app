using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiApp.DTOs;
using CoreApiApp.Models;
using CoreApiApp.Services;
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
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager, IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                return BadRequest("Some model properties are not valid");
            }
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
