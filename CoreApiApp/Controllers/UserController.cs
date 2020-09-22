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

        [Authorize]
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

        //method to generate token
        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] EmailDto model)
        {
            //search database for a user with inputted email
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);

            //create claims
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            //obtain JWT secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            //generate signin credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //create security token descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //how many days before token expires
                SigningCredentials = creds
            };

            //build token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //create token
            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });

        }

    }
}
