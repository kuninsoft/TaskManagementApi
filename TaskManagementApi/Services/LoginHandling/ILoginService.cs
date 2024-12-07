using System.IdentityModel.Tokens.Jwt;
using TaskManagementApi.Models.Login;

namespace TaskManagementApi.Services.LoginHandling;

public interface ILoginService
{
    Task<string> Login(LoginRequestDto loginRequestDto);
}