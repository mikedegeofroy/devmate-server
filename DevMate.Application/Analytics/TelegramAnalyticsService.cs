using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Analytics;

namespace DevMate.Application.Analytics;

public class TelegramAnalyticsService : ITelegramAnalyticsService
{
    private readonly ITelegramService _telegramService;

    public TelegramAnalyticsService(ITelegramService telegramService)
    {
        _telegramService = telegramService;
    }

    public async Task<TelegramAnalyticsData> GetActivityData(long id)
    {
        IEnumerable<TelegramMessageModel> rawData =
            await _telegramService.GetMessagesByChatIdAsync(id, DateTime.Today.AddDays(-14));

        var data = new List<TelegramDataPoint>();

        var groupedData = rawData
            .GroupBy(x => x.DateTime.Date)
            .ToDictionary(g => g.Key, g => g.Count());

        for (int i = 0; i < 14; ++i)
        {
            DateTime date = DateTime.Now.Date.AddDays(-i);
            int count = groupedData.GetValueOrDefault(date);

            data.Add(new TelegramDataPoint(count, date));
        }

        return new TelegramAnalyticsData(data.ToArray());
    }

    public async Task<IEnumerable<TelegramUserModel>> GetMostActiveUsers(long id)
    {
        IEnumerable<TelegramMessageModel> rawData =
            await _telegramService.GetMessagesByChatIdAsync(id, DateTime.Today.AddDays(-14));

        var groupedData = rawData
            .GroupBy(x => x.Peer)
            .ToDictionary(g => g.Key, g => g.Count());

        return groupedData
            .OrderByDescending(x => x.Value)
            .Select(x => new TelegramUserModel(x.Key.Id, x.Key.Username ?? "N/A", x.Key.DisplayName, x.Key.Photo, 0));
    }
}