using Microsoft.AspNetCore.Mvc;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Presentation.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserActivityDataController : ControllerBase
{

    private readonly IAnalyticsService _analyticsService;

    public UserActivityDataController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TelegramUserModel>>> Get()
    {
        IEnumerable<TelegramUserModel> users = await _analyticsService.GetMostActiveUsers(1600634396);
        return Ok(users);
    }
}