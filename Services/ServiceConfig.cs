using Alarm_Project.AutoMapper;
using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Services.Contracts;
using Hangfire;
using TokenHandler = Alarm_Project.JWT.TokenHandler;

namespace Alarm_Project.Services;

public static class ServiceConfig
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<ProductRepository>();
        services.AddScoped<AlarmRepository>();
        services.AddScoped<PaymentRepository>();
        services.AddScoped<Alarm>(); 
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<Users>();
        services.AddScoped<Products>();
        services.AddScoped<Payment>();
        services.AddScoped<AlarmSettings>();
        services.AddScoped<IAlarmRepository<Alarm>, AlarmRepository>();
        services.AddScoped<IPaymentRepository<Payment>, PaymentRepository>();
        services.AddHttpContextAccessor();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAlarmService, AlarmService>();
        services.AddScoped<AlarmDto>();
        services.AddScoped<SlackService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddControllers();
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        services.AddScoped<TokenHandler>();
        services.AddHangfire(config => config.UseSqlServerStorage("sqlConnection"));
        services.AddHangfire(config => config.UseInMemoryStorage());
        services.AddHangfireServer();
    }

}