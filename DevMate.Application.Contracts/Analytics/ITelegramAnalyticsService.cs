using DevMate.Application.Models;
using DevMate.Application.Models.Analytics;

namespace DevMate.Application.Contracts.Analytics;

public interface ITelegramAnalyticsService
{
    Task<TelegramAnalyticsData> GetActivityData(long id);

    Task<IEnumerable<TelegramUserModel>> GetMostActiveUsers(long id);
}