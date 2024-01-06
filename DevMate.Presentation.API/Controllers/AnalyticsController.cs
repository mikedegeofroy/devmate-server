using Microsoft.AspNetCore.Mvc;
using ParkingApp.Application.Contracts;
using ParkingApp.Application.Models;

namespace ParkingApp.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(ILogger<AnalyticsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetActivityData")]
    public IEnumerable<DataPoint> Get()
    {
        var mockData = new List<DataPoint>
        {
            new DataPoint(0),
            new DataPoint(1),
            new DataPoint(2),
            new DataPoint(3),
        };
        
        return mockData;
    }
}