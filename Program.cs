using System.Text;
using Alarm_Project.Repositories;
using Alarm_Project.Repositories.DbRepo;
using Alarm_Project.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TokenHandler = Alarm_Project.JWT.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomServices();

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.Configure<SlackOptions>(builder.Configuration.GetSection("Slack"));
builder.Services.AddScoped<SlackService>();

//JWT*************************
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecurityKey"]!)),
        ClockSkew = TimeSpan.Zero
    };

});
builder.Services.AddSingleton<TokenHandler>();

//JWT*************************

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", async context =>
{
    var slackService = context.RequestServices.GetRequiredService<SlackService>();
    await slackService.SendSlackMessage("Merhaba, bu bir test mesajıdır!");
    await context.Response.WriteAsync("Message sent to Slack!");
});


app.Run();
