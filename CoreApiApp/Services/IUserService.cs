using CoreApiApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterDto dto);
    }
}
