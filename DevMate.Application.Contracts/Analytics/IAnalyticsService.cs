namespace ParkingApp.Application.Contracts.Analytics;

public interface IAnalyticsService
{
    IEnumerable<Models.DataPoint> GetActivityData();
}