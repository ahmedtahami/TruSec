using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;
using TruSec.DAL.Entities;

namespace TruSec.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUserRegistrationDto userDto)
        {
            var userCreationResult = await _userService.CreateUserAsync(userDto);
            if (!userCreationResult.Success)
            {
                return BadRequest(userCreationResult);
            }
            var emailResult = await _userService.SendConfirmationEmailAsync(userDto.Email);
            if (!emailResult.Success)
            {
                userCreationResult.Message += emailResult.Message; 
            }
            return Ok(userCreationResult);
        }
    }
}
