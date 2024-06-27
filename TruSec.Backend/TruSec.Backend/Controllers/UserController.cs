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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAsync();
            if (!users.Success)
            {
                return BadRequest(users);
            }
            return Ok(users.Response);
        }

        [HttpPost]
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
            return Ok(userCreationResult.Response);
        }
    }
}
