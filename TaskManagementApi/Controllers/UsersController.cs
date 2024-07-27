using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        return Ok(new [] { new User(), new User() });
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        return Ok(new User());
    }

    [HttpPost]
    public IActionResult Post([FromBody] User value)
    {
        return Created("none", value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] User value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}