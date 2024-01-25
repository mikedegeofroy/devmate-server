using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Analytics;

public class TelegramAnalyticsService : ITelegramAnalyticsService
{
    private readonly ITelegramClientService _telegramClientService;
    private readonly IUserRepository _userRepository;

    public TelegramAnalyticsService(ITelegramClientService telegramClientService, IUserRepository userRepository)
    {
        _telegramClientService = telegramClientService;
        _userRepository = userRepository;
    }

    public async Task<TelegramAnalyticsData> GetActivityDataAsync(long id, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        IEnumerable<TelegramMessageModel> rawData =
            await _telegramClientService.GetMessagesByChatIdAsync(id, DateTime.Today.AddDays(-14), user, store);

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

    public async Task<IEnumerable<TelegramUserModel>> GetMostActiveUsersAsync(long id, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        IEnumerable<TelegramMessageModel> rawData =
            await _telegramClientService.GetMessagesByChatIdAsync(id, DateTime.Today.AddDays(-14), user, store);

        var groupedData = rawData
            .GroupBy(x => x.Peer)
            .ToDictionary(g => g.Key, g => g.Count());

        return groupedData
            .OrderByDescending(x => x.Value)
            .Select(x => new TelegramUserModel(x.Key.Id, x.Key.Username ?? "N/A", x.Key.DisplayName, x.Key.Photo, 0));
    }

    public async Task<IEnumerable<TelegramUserModel>> GetPeersAsync(UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        IEnumerable<TelegramPeerModel> peers = await _telegramClientService.GetPeersAsync(user, store);

        return peers.Select(x =>
            new TelegramUserModel(x.Id, x.Username ?? "N/A", x.DisplayName, x.Photo, x.AccessHash));
    }
}