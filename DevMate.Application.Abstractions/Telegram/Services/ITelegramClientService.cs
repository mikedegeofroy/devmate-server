using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Abstractions.Telegram.Services;

public interface ITelegramClientService
{
    Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit,
        UserDto user, Stream store);

    Task<IEnumerable<TelegramPeerModel>> GetPeersAsync(UserDto user, Stream store);
    void Send(TelegramUserModel recipient, string message, UserDto user, Stream store);
    TelegramUserModel GetUser(string phone, Stream store);
}