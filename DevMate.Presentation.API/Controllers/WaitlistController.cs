using DevMate.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("waitlist")]
public class WaitlistController : ControllerBase
{
    private readonly IWaitlistService _waitlistService;

    public WaitlistController(IWaitlistService waitlistService)
    {
        _waitlistService = waitlistService;
    }

    [HttpGet]
    public ActionResult AddToWaitlist(string username)
    {
        _waitlistService.AddUser(username);
        return Ok();
    }
}