using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreApiApp.DTOs;
using CoreApiApp.Models;
using CoreApiApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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

        // /user/register
        [AllowAnonymous]
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

        // /user/login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto model)
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

        //[HttpGet]
        //public IActionResult GetAllLoggedInUsers()
        //{

        //}

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
