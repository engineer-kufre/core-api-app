using CoreApiApp.DTOs;
using CoreApiApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterDto model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            if(model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password does not match",
                    IsSuccess = false
                };
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

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
