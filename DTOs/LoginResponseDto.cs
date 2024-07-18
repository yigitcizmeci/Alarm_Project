using Alarm_Project.Models;
using NuGet.Common;

namespace Alarm_Project.DTOs;

public record LoginResponseDto(Users Users,string Message, string Token);