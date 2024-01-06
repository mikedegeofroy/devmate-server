using ParkingApp.Application.Abstractions.Repositories;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Application.Analytics;

public class AnalyticsService : IAnalyticsService
{
    public AnalyticsService()
    {
    }

    public IEnumerable<DataPoint> GetActivityData()
    {
        throw new NotImplementedException();
    }
}
