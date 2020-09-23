using CoreApiApp.DTOs;
using System.Threading.Tasks;

namespace CoreApiApp.Services
{
    //UserService interface
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterDto dto);
        Task<UserManagerResponse> LoginUserAsync(LoginDto dto);
    }
}
