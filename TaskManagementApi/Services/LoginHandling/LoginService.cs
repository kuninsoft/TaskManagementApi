using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using TaskManagementApi.Data;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Models.Login;
using TaskManagementApi.Models.User;
using TaskManagementApi.Services.UserHandling;

namespace TaskManagementApi.Services.LoginHandling;

public class LoginService(IUserRepository repository) : ILoginService
{
    public async Task<string> Login(LoginRequestDto loginRequestDto)
    {
        User user = await repository.GetByFilterAsync(user => user.Username == loginRequestDto.Username) 
                     ?? throw new AuthenticationFailureException("Invalid username or password");

        if (user.PasswordHash != loginRequestDto.Password)
        {
            throw new AuthenticationFailureException("Invalid username or password");
        }
        
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username)};

        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}