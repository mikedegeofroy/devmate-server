using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

using ITelegramBotClient = DevMate.Application.Abstractions.Telegram.Services.BotClients.ITelegramBotClient;

namespace DevMate.Infrastructure.Integration.Telegram.Bot.Services;

public class TelegramBotClient : ITelegramBotClient
{
    public Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit, UserDto user, Stream store)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TelegramPeerModel>> GetPeersAsync(UserDto user, Stream store)
    {
        throw new NotImplementedException();
    }

    public void Send(TelegramUserModel recipient, string message, UserDto user, Stream store)
    {
        throw new NotImplementedException();
    }

    public TelegramUserModel GetUser(string phone, Stream store)
    {
        throw new NotImplementedException();
    }

    public void Subscribe()
    {
        throw new NotImplementedException();
    }
}