using Alarm_Project.DTOs;
using Alarm_Project.Models;

namespace Alarm_Project.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<Users>> GetAllUserAsync();
    Task<Users> GetByIdAsync(Guid UserId);
    Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<LoginResponseDto> UserLoginAsync(UserLoginDto userLoginDto);
    Task<bool> DeleteAllUserAsync();
    Task<bool> DeleteUserByIdAsync(Guid UserId);
}