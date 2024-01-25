using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Contracts.Analytics;

public interface ITelegramAnalyticsService
{
    Task<TelegramAnalyticsData> GetActivityDataAsync(long id, UserDto user);

    Task<IEnumerable<TelegramUserModel>> GetMostActiveUsersAsync(long id, UserDto user);

    Task<IEnumerable<TelegramUserModel>> GetPeersAsync(UserDto user);
}