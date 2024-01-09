using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Models;
using DevMate.Application.Models.Analytics;

namespace DevMate.Application.Abstractions.Telegram.Services;

public interface ITelegramService
{
    Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit);

    Task<IEnumerable<TelegramPeerModel>> GetPeersAsync();

    void Send(TelegramUserModel user, string message);
}