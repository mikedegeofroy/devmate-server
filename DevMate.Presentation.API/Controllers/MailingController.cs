using Microsoft.AspNetCore.Mvc;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Presentation.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MailingController : ControllerBase
{
    private readonly IMailingService _mailingService;

    public MailingController(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    [HttpPost]
    public async Task<ActionResult> Post(TelegramUserModel[] recipients, string message)
    {
        _mailingService.Send(recipients, message);
        return Ok();
    }
}