namespace DevMate.Application.Abstractions.Telegram.Services.UserClients;

public interface ITelegramUserClientFactory
{
    ITelegramUserClient GetClient();
}