namespace Alarm_Project.DTOs;

public record ProductCreateDto
{
    public Guid ProductId { get; init; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescripcion { get; set; }
    public int ProductPrice { get; set; }
}