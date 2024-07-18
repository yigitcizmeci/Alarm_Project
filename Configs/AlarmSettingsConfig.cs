using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarm_Project.Configs;

public class AlarmSettingsConfig : IEntityTypeConfiguration<AlarmSettings>
{
    public void Configure(EntityTypeBuilder<AlarmSettings> builder)
    {
        builder.HasKey(p => p.AlarmId);
        builder.Property(up => up.UserId).IsRequired();
        builder.Property(up => up.ReceiveReport).IsRequired();
        builder.Property(up => up.ReceiveEmail).IsRequired();
        builder.Property(up => up.ReceiveSlack).IsRequired();
        
        var alarmId = new Guid("11111111-1111-1111-1111-111111111111");
        var alarm2Id = new Guid("22222222-2222-2222-2222-222222222222");
        builder.HasData(
            new AlarmSettings()
            {
                AlarmId = alarm2Id, 
                UserId = alarmId,
                ReceiveEmail = false,
                ReceiveReport = false,
                ReceiveSlack = false,
            });
    }
}