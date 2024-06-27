using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;
using TruSec.DAL.Entities;
using TruSec.DAL.Repositories;

namespace TruSec.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<ResponseResult<ApplicationUserRegistrationDto>> CreateUserAsync(ApplicationUserRegistrationDto userDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    IsDeleted = false,
                    UserName = userDto.Email
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, userDto.RoleName);
                    if (roleResult.Succeeded)
                    {
                        return new ResponseResult<ApplicationUserRegistrationDto> { Message = "User created successfully.", Response = userDto, Success = true };
                    }
                    return new ResponseResult<ApplicationUserRegistrationDto> { Message = "User created successfully, but role assignment failed.", Response = userDto, Success = true };
                }
                return new ResponseResult<ApplicationUserRegistrationDto> { Success = false, Response = userDto, Message = "User creation failed." };
            }
            catch (Exception)
            {
                return new ResponseResult<ApplicationUserRegistrationDto> { Success = false, Response = userDto, Message = "User creation failed." };
            }
        }
        public async Task<ResponseResult<List<ApplicationUserDto>>> GetAsync()
        {
            try
            {
                var users = await _unitOfWork.ApplicationUsers.GetAllAsync(false);
                var result = _mapper.Map<List<ApplicationUserDto>>(users.ToList());
                return new ResponseResult<List<ApplicationUserDto>> { Success = true, Response = result, Message = "Success" };
            }
            catch (Exception)
            {
                return new ResponseResult<List<ApplicationUserDto>> { Success = false, Message = "Failed to reterive users." };
            }
        }

        public async Task<ResponseResult<object>> SendConfirmationEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new ResponseResult<object> { Success = false, Message = "Could not sent confirmation email." };
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{_configuration.GetRequiredSection("ResetPassowrdLink").Value}?userId={user.Id}&token={Uri.EscapeDataString(token)}";
                var subject = "Confirm your email";
                var message = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";
                await _emailService.SendEmailAsync(user.Email, subject, message);
                return new ResponseResult<object> { Success = true, Message = "Confirmation email sent successfully." };
            }
            catch (Exception)
            {
                return new ResponseResult<object> { Success = false, Message = "Could not sent confirmation email." };
            }
        }
    }
}
