using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alarm_Project.Models;
using Microsoft.IdentityModel.Tokens;

namespace Alarm_Project.JWT;

public class TokenHandler(IConfiguration configuration)
{
    public string CreateToken(Users Users)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Users.UserName),
            new Claim(ClaimTypes.Email, Users.Email),
            new Claim(ClaimTypes.NameIdentifier, Users.UserId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("AppSettings:SecurityKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: configuration.GetSection("AppSettings:Issuer").Value,
            audience: configuration.GetSection("AppSettings:Audience").Value,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}