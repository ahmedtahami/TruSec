using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TruSec.BLL.DTOs;
using TruSec.BLL.Services;

namespace TruSec.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<ApplicationUserRegistrationDto>> CreateUserAsync(ApplicationUserRegistrationDto userDto);
        Task<ResponseResult<object>> SendConfirmationEmailAsync(string email);
        Task<ResponseResult<object>> SendResetPasswordEmailAsync(string email);
        Task<ResponseResult<List<ApplicationUserDto>>> GetAsync();
    }
}
