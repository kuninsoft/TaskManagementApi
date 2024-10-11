using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services.UserHandling;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<UserDto> users = await userService.GetAllUsers();

        if (users.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            UserDto user = await userService.GetUser(id);

            return Ok(user);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        UserDto createdUser = await userService.CreateUser(value);

        return Created(Url.Action(nameof(Get), new { id = createdUser.Id }), createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await userService.UpdateUser(id, value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await userService.DeleteUser(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
        return Ok();
    }
}