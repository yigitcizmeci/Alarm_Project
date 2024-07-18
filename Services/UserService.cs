using Alarm_Project.DTOs;
using Alarm_Project.JWT;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Services.Contracts;
using AutoMapper;
using NuGet.Common;

namespace Alarm_Project.Services;

public class UserService(UserRepository userRepository, IMapper mapper, TokenHandler tokenHandler) : IUserService
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly TokenHandler _tokenHandler = tokenHandler;

    public async Task<IEnumerable<Users>> GetAllUserAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<Users> GetByIdAsync(Guid UserId)
    {
        return await _userRepository.GetByIdAsync(UserId);
    }

    public async Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var first = _mapper.Map<Users>(userCreateDto);
        var createUser = await _userRepository.CreateUserAsync(first);
        var user = _mapper.Map<UserCreateDto>(createUser);
        return user;
    }

    public async Task<LoginResponseDto> UserLoginAsync(UserLoginDto userLoginDto)
    { 
        var login = await _userRepository.LoginUserAsync(userLoginDto);
        var token = _tokenHandler.CreateToken(login);
        var loginResult = new LoginResponseDto(login, "Login Successful",token);
        return _mapper.Map<LoginResponseDto>(loginResult);
    }

    public async Task<bool> DeleteAllUserAsync()
    {
        return await _userRepository.DeleteAllUserAsync();
    }

    public async Task<bool> DeleteUserByIdAsync(Guid UserId)
    {
        return await _userRepository.DeleteUserByIdAsync(UserId);
    }
}