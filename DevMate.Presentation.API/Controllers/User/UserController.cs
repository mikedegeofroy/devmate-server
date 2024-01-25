using DevMate.Application.Contracts.User;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers.User;

[ApiController]
[Route("/api/user/")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("me")]
    public ActionResult<UserDto> Me()
    {
        var user = new UserDto(User.Identity?.Name ?? string.Empty, string.Empty, string.Empty, string.Empty);
        return Ok(_userService.GetUser(user).GetAwaiter().GetResult());
    }
}