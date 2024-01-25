using DevMate.Application.Abstractions.Telegram.Models;

namespace DevMate.Application.Abstractions.Telegram.Services;

public interface ITelegramClientBuilder
{
    Task<TelegramClientModel> Build();
}