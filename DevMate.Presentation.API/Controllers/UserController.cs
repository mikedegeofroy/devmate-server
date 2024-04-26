using System.Security.Claims;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public ActionResult<UserDto> GetMe()
    {
        string? userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
            return Ok(_userService.GetUser(long.Parse(userId)));
        return NotFound();
    }
    
    [HttpGet("events")]
    public ActionResult<UserDto> GetEvents()
    {
        string? userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null)
            return NotFound();
        
        UserDto dto = _userService.GetUser(long.Parse(userId));
        return Ok(_userService.GetEvents(dto));
    }
}