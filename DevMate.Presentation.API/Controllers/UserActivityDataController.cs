using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserActivityDataController : ControllerBase
{

    private readonly ITelegramAnalyticsService _telegramAnalyticsService;

    public UserActivityDataController(ITelegramAnalyticsService telegramAnalyticsService)
    {
        _telegramAnalyticsService = telegramAnalyticsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TelegramUserModel>>> Get()
    {
        IEnumerable<TelegramUserModel> users = await _telegramAnalyticsService.GetMostActiveUsers(1600634396);
        return Ok(users);
    }
}