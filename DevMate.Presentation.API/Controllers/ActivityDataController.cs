using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ActivityDataController : ControllerBase
{
    private readonly ILogger<ActivityDataController> _logger;
    private readonly ITelegramAnalyticsService _telegramAnalyticsService;

    public ActivityDataController(ILogger<ActivityDataController> logger, ITelegramAnalyticsService telegramAnalyticsService)
    {
        _logger = logger;
        _telegramAnalyticsService = telegramAnalyticsService;
    }

    [HttpPost]
    public async Task<ActionResult<TelegramAnalyticsData>> Post(long id)
    {
        TelegramAnalyticsData data = await _telegramAnalyticsService.GetActivityData(id);
        return Ok(data);
    }
}