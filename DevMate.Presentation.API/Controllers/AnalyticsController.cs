using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("/api/analytics/")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly ITelegramAnalyticsService _telegramAnalyticsService;

    public AnalyticsController(ITelegramAnalyticsService telegramAnalyticsService)
    {
        _telegramAnalyticsService = telegramAnalyticsService;
    }

    [HttpPost("peer-activity")]
    public ActionResult<TelegramAnalyticsData> ActivityByPeerId(long id)
    {
        var user = new UserDto(User.Identity?.Name ?? string.Empty, string.Empty, string.Empty, string.Empty);
        TelegramAnalyticsData data = _telegramAnalyticsService.GetActivityDataAsync(id, user).GetAwaiter().GetResult();
        return Ok(data);
    }

    [HttpPost("user-activity")]
    public ActionResult<IEnumerable<TelegramUserModel>> ActivityPerUser(long id)
    {
        var user = new UserDto(User.Identity?.Name ?? string.Empty, string.Empty, string.Empty, string.Empty);
        IEnumerable<TelegramUserModel> data = _telegramAnalyticsService.GetMostActiveUsersAsync(id, user).GetAwaiter()
            .GetResult();
        return Ok(data);
    }

    [HttpGet("peers")]
    public ActionResult<IEnumerable<TelegramUserModel>> Peers()
    {
        var user = new UserDto(User.Identity?.Name ?? string.Empty, string.Empty, string.Empty, string.Empty);
        IEnumerable<TelegramUserModel> data = _telegramAnalyticsService.GetPeersAsync(user).GetAwaiter().GetResult();
        return Ok(data);
    }
}