namespace DevMate.Application.Abstractions.Telegram.Services.BotClients;

public interface ITelegramBotClientFactory
{
    ITelegramBotClient GetClient();
}