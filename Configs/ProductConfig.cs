using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarm_Project.Configs;

public class ProductConfig : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.ProductDescripcion).IsRequired();
        builder.Property(p => p.ProductPrice).IsRequired();
        
        var user1Id = new Guid("11111111-1111-1111-1111-111111111111");
        var user2Id = new Guid("22222222-2222-2222-2222-222222222222");
        builder.HasData(
            new Products
            {
                ProductId = user2Id,
                UserId = user1Id,
                ProductName = "Mangal",
                ProductDescripcion = "Ateş seni çağırıyor",
                ProductPrice = 700
            }
        );
    }
}