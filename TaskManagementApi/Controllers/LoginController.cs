using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Login;
using TaskManagementApi.Services.LoginHandling;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class LoginController(ILoginService loginService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginRequestDto body)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            return Ok(await loginService.Login(body));
        }
        catch (AuthenticationFailureException)
        {
            return Challenge();
        }
    }
}