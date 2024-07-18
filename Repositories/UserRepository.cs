using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Repositories.DbRepo;
using Microsoft.EntityFrameworkCore;

namespace Alarm_Project.Repositories;

public class UserRepository(RepositoryContext repositoryContext) : IUserRepository<Users>
{
    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        var allUsers = await repositoryContext.Users.ToListAsync();
        if (allUsers.Count == 0)
        {
            throw new Exception("Users are missing");
        }

        return allUsers.ToList();
    }

    public async Task<Users> GetByIdAsync(Guid Id)
    {
        var UserId = await repositoryContext.Users.FirstOrDefaultAsync(p => p.UserId == Id);
        if (UserId == null)
        {
            throw new KeyNotFoundException($"User with ID {Id} was not found.");
        }

        await repositoryContext.FindAsync<Users>(Id);
        return UserId;
    }

    public async Task<Users> CreateUserAsync(Users newUser)
    {
        var existUser = await repositoryContext.Users.FirstOrDefaultAsync(p =>
            p.UserName == newUser.UserName || p.Email == newUser.Email);
        if (existUser != null)
        {
            throw new InvalidDataException("User exist in system");
        }

        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        await repositoryContext.Users.AddAsync(newUser);
        await repositoryContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<Users> LoginUserAsync(UserLoginDto userLoginDto)
    {
        var existUser =
            await repositoryContext.Users.FirstOrDefaultAsync(p =>
                p.UserName == userLoginDto.UserName);
        if (existUser == null && BCrypt.Net.BCrypt.Verify(userLoginDto.Password,existUser?.Password))
        {
            throw new InvalidDataException("Wrong username or Password");
        }

        return existUser;
    }

    public async Task<bool> DeleteAllUserAsync()
    {
        var allUsers = await repositoryContext.Users.ToListAsync();
        if (allUsers.Count == 0)
        {
            throw new KeyNotFoundException("There is no any User to delete");
        }

        repositoryContext.Users.RemoveRange(allUsers);
        await repositoryContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserByIdAsync(Guid UserId)
    {
        var existUser = await repositoryContext.Users.FirstOrDefaultAsync(p => p.UserId == UserId);
        if (existUser == null)
        {
            throw new KeyNotFoundException($"User with ID {UserId} was not found.");
        }

        repositoryContext.Users.Remove(existUser);
        await repositoryContext.SaveChangesAsync();
        return true;
    }
}