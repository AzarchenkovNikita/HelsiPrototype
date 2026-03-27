using HelsiPrototype.DTO;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using Microsoft.AspNetCore.Mvc;

namespace HelsiPrototype.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserObject dto)
    {
        string userId = await _service.CreateAsync(dto.UserName);
        return Ok(new { userId });
    }

    [HttpGet("getrange")]
    public async Task<IActionResult> GetRange()
    {
        List<User> userRange = await _service.GetRangeAsync();
        return Ok(new { userRange });
    }
}
