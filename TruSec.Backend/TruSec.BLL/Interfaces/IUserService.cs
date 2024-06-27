using TruSec.BLL.DTOs;

namespace TruSec.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<ApplicationUserRegistrationDto>> CreateUserAsync(ApplicationUserRegistrationDto userDto);
        Task<ResponseResult<object>> SendConfirmationEmailAsync(string username);
    }
}
