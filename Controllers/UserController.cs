using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Services;
using Alarm_Project.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alarm_Project.Controllers
{
    [Route("api/user_options")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userService.GetAllUserAsync());
        }

        [HttpGet("{_id:guid}")]
        public async Task<IActionResult> GetUsersById(Guid _id)
        {
            return Ok(await userService.GetByIdAsync(_id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllUsers()
        {
            return Ok(await userService.DeleteAllUserAsync());
        }
        [Authorize]
        [HttpDelete("{_id:guid}")]
        public async Task<IActionResult> DeleteUserById(Guid _id)
        {
            return Ok(await userService.DeleteUserByIdAsync(_id));
        }
    }
}