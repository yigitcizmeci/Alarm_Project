namespace Alarm_Project.Models;

public class Products
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescripcion { get; set; }
    public int ProductPrice { get; set; }
    public Users Users { get; set; }
}