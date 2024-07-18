using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarm_Project.Configs;

public class AlarmConfig : IEntityTypeConfiguration<Alarm>
{
    public void Configure(EntityTypeBuilder<Alarm> builder)
    {
            builder.HasKey(a => a.AlarmId);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.AlarmType).IsRequired();
            builder.Property(a => a.Time).IsRequired(); 
            builder.Property(a => a.AlarmMessage).IsRequired();
            var alarmId = new Guid("11111111-1111-1111-1111-111111111111");
            var alarm2Id = new Guid("22222222-2222-2222-2222-222222222222");
            builder.HasData(
                new Alarm()
            {
                AlarmId = alarm2Id,
                UserId = alarmId,
                AlarmType = "Warning",
                Time = DateTime.Now,
                AlarmMessage = "Exception"
            });
    }
}