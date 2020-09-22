using CoreApiApp.DTOs;
using CoreApiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiApp.Services
{
    //UserService class implements IUserService interface
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public Task<UserManagerResponse> LoginUserAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        //method to login a user
        //public async Task<UserManagerResponse> LoginUserAsync(LoginDto model)
        //{
        //    //search database for a user with inputted email
        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    //check that a user with that email exists in the database
        //    if (user == null)
        //    {
        //        return new UserManagerResponse
        //        {
        //            Message = "Email does not exist!",
        //            IsSuccess = false
        //        };
        //    }

        //    //check that returned user's password matches that inputted
        //    var result = await _userManager.CheckPasswordAsync(user, model.Password);

        //    //return response if password is invalid
        //    if (!result)
        //    {
        //        return new UserManagerResponse
        //        {
        //            Message = "Invalid Password!",
        //            IsSuccess = false
        //        };
        //    }

        //    //var claims = new[]
        //    //{
        //    //    new Claim("Email", model.Email),
        //    //    new Claim(ClaimTypes.NameIdentifier, user.Id)
        //    //};

        //    //var token = new JwtSecurityToken(
        //    //    issuer: _configuration["Auth"]
        //    //    )

        //}

        //method to register a user
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterDto model)
        {
            //check model object exists
            if (model == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            //confirm password
            if(model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password does not match",
                    IsSuccess = false
                };
            }

            //create a user from data transfer object model
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };

            //add user to database
            var result = await _userManager.CreateAsync(user, model.Password);

            //return success or error message
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created",
                    IsSuccess = true
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = "User was not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                }; 
            }
        }

       
    }
}
