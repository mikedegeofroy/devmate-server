using ParkingApp.Application.Abstractions.Telegram.Models;
using ParkingApp.Application.Abstractions.Telegram.Services;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Application.Analytics;

public class AnalyticsService : IAnalyticsService
{
    private readonly ITelegramService _telegramService;

    public AnalyticsService(ITelegramService telegramService)
    {
        _telegramService = telegramService;
    }

    public async Task<AnalyticsData> GetActivityData(long id)
    {
        IEnumerable<TelegramMessageModel> rawData =
            await _telegramService.GetMessagesByChatIdAsync(id, DateTime.Today.AddDays(-14));

        var data = new List<DataPoint>();

        var groupedData = rawData
            .GroupBy(x => x.DateTime.Date)
            .ToDictionary(g => g.Key, g => g.Count());

        for (int i = 0; i < 14; ++i)
        {
            DateTime date = DateTime.Now.Date.AddDays(-i);
            int count = groupedData.GetValueOrDefault(date);

            data.Add(new DataPoint(count, date));
        }

        return new AnalyticsData(data.ToArray());
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