using Alarm_Project.Configs;
using Alarm_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Alarm_Project.Repositories.DbRepo;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Alarm> Alarm { get; set; }
    public DbSet<AlarmSettings> AlarmSettings { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new AlarmConfig());
        modelBuilder.ApplyConfiguration(new AlarmSettingsConfig());
        modelBuilder.ApplyConfiguration(new PaymentConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.Entity<Alarm>()
            .HasOne(a => a.AlarmSettings)
            .WithOne(p => p.Alarm)
            .HasForeignKey<AlarmSettings>(a => a.AlarmId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Users>()
            .HasOne(u => u.AlarmSettings)
            .WithOne(a => a.Users)
            .HasForeignKey<AlarmSettings>(a => a.UserId)
            .IsRequired();
        modelBuilder.Entity<Users>()
            .HasOne(u => u.Payment)
            .WithOne(p => p.Users)
            .HasForeignKey<Payment>(p => p.UserId)
            .IsRequired();
        modelBuilder.Entity<Users>()
            .HasOne(u => u.Products)
            .WithOne(p => p.Users)
            .HasForeignKey<Products>(p => p.UserId)
            .IsRequired();
    }
}