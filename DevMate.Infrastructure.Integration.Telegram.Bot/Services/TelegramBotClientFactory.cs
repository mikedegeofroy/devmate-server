using DevMate.Application.Abstractions.Telegram.Services.BotClients;
using ITelegramBotClient = DevMate.Application.Abstractions.Telegram.Services.BotClients.ITelegramBotClient;

namespace DevMate.Infrastructure.Integration.Telegram.Bot.Services;

public class TelegramBotClientFactory : ITelegramBotClientFactory
{
    public ITelegramBotClient GetClient()
    {
        throw new NotImplementedException();
    }
}