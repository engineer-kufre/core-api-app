using CoreApiApp.DTOs;
using CoreApiApp.Models;
using CoreApiApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _configuration = configuration;
        }

        //method to register a new user
        // /user/register
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
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

        //method to login an existing user
        // /user/login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

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

        //method to fetch the details of the loggedin user
        // /user/getloggedinuserdetails
        [HttpGet("GetLoggedInUserDetails")]
        public async Task<IActionResult> GetLoggedInUserDetailsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = new ReturnedUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedOn = user.CreatedOn
            };
            return Ok(result);
        }

        //method to fetch all registered users
        // /user/getallregisteredusers
        [HttpGet("GetAllRegisteredUsers")]
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
