using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Services;
using Alarm_Project.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alarm_Project.Controllers
{
    [Route("api/register_login")]
    [ApiController]
    public class Register_Login(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            return Ok(await _userService.CreateUserAsync(userCreateDto));
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDto _userLoginDto)
        {
            return Ok(await _userService.UserLoginAsync(_userLoginDto));
        }
    }
}
