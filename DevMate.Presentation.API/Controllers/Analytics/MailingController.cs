using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers.Analytics;

[ApiController]
[Route("/api/mailing/")]
[Authorize]
public class MailingController : ControllerBase
{
    private readonly IMailingService _mailingService;

    public MailingController(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    [HttpPost("send")]
    public Task<ActionResult> Send(TelegramUserModel[] recipients, string message)
    {
        var user = new UserDto(
            User.Identity?.Name ?? string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);
        _mailingService.Send(recipients, message, user);
        return Task.FromResult<ActionResult>(Ok());
    }
}