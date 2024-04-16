using DevMate.Application.Abstractions.Telegram.Services.UserClients;

namespace DevMate.Application.Abstractions.Telegram.Services.BotClients;

public interface ITelegramBotClientFactory
{
    ITelegramBotClient GetClient();
}