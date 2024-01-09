using Microsoft.AspNetCore.Mvc;
using ParkingApp.Application.Contracts;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Presentation.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ActivityDataController : ControllerBase
{
    private readonly ILogger<ActivityDataController> _logger;
    private readonly IAnalyticsService _analyticsService;

    public ActivityDataController(ILogger<ActivityDataController> logger, IAnalyticsService analyticsService)
    {
        _logger = logger;
        _analyticsService = analyticsService;
    }

    [HttpGet]
    public async Task<ActionResult<AnalyticsData>> Get()
    {
        AnalyticsData data = await _analyticsService.GetActivityData(1600634396);
        return Ok(data);
    }
}