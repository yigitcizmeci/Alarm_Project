using Alarm_Project.DTOs;
using Alarm_Project.Models;

namespace Alarm_Project.Repositories.Contracts;

public interface IUserRepository<T>
{
    Task<IEnumerable<T>> GetAllUsersAsync();
    Task<T> GetByIdAsync(Guid Id);
    Task<T> CreateUserAsync(T entity);
    Task<T> LoginUserAsync(UserLoginDto userLoginDto);
    Task<bool> DeleteAllUserAsync();
    Task<bool> DeleteUserByIdAsync(Guid UserId);
}