namespace Alarm_Project.Services;

public class apiResponse<T>(bool success, string message, T data)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public T Data { get; set; } = data;
    
}