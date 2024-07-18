using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarm_Project.Configs;

public class UserConfig : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.HasKey(p => p.UserId);
        builder.Property(p => p.UserName).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Password).IsRequired();
        
        var user1Id = new Guid("11111111-1111-1111-1111-111111111111");
        builder.HasData(
            new Users
            {
                UserId = user1Id, 
                Email = "yigitcizmeci@hotmail.com", 
                UserName = "yigit",
                Password = "cizmeci",
            });
    }
}