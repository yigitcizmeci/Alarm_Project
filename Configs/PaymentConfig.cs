using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarm_Project.Configs;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentId);
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.CardOwnerName).IsRequired();
        builder.Property(p => p.CardNumber).IsRequired();
        builder.Property(p => p.ExpireYear).IsRequired();
        builder.Property(p => p.CVV).IsRequired();
        builder.Property(p => p.Currency).IsRequired();

        var user1Id = new Guid("11111111-1111-1111-1111-111111111111");
        var user2Id = new Guid("22222222-2222-2222-2222-222222222222");
        builder.HasData(
            new Payment
            {
                PaymentId = user2Id, 
                UserId = user1Id, 
                CardOwnerName = "Test Kart", 
                CardNumber = "4355084355084358", 
                ExpireYear = "2030", 
                CVV = "000", 
                Currency = 2000
            }
        );
    }
}