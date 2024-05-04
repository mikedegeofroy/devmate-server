using DevMate.Application.Contracts;
using DevMate.Application.Models.Domain;
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

    [HttpPost]
    public ActionResult AddToWaitlist([FromBody] WaitlistUser user)
    {
        _waitlistService.AddUser(user);
        return Ok();
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<WaitlistUser>> GetWaitlist()
    {
        return Ok(_waitlistService.GetUsers());
    }
}