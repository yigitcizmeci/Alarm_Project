using Alarm_Project.Models;

namespace Alarm_Project.DTOs;

public record UserCreateDto(string Email, string UserName, string Password);