using ParkingApp.Application.Abstractions.Telegram.Models;
using ParkingApp.Application.Abstractions.Telegram.Services;
using ParkingApp.Application.Models;

namespace DevMate.Infrastructure.Integration.Telegram.Services;

public class CachingTelegramService : ITelegramService
{
    private readonly ITelegramService _telegramService = new TelegramService();
    private readonly Dictionary<long, IEnumerable<TelegramMessageModel>> _cache = new();

    public async Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit)
    {
        IEnumerable<TelegramMessageModel>? cached = _cache.GetValueOrDefault(id);
        if (cached != null) return cached;

        _cache[id] = await _telegramService.GetMessagesByChatIdAsync(id, limit);
        return _cache[id];
    }

    public async Task<IEnumerable<TelegramPeerModel>> GetPeersAsync()
    {
        return await _telegramService.GetPeersAsync();
    }

    public void Send(TelegramUserModel user, string message)
    {
        _telegramService.Send(user, message);
    }
}