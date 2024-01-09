using ParkingApp.Application.Models;

namespace ParkingApp.Application.Contracts.Analytics;

public interface IAnalyticsService
{
    Task<AnalyticsData> GetActivityData(long id);

    Task<IEnumerable<Models.TelegramUserModel>> GetMostActiveUsers(long id);
}