using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models;
using DevMate.Application.Models.Analytics;

namespace DevMate.Application.MailingService;

public class MailingService : IMailingService
{
    private readonly ITelegramService _telegramService;

    public MailingService(ITelegramService telegramService)
    {
        _telegramService = telegramService;
    }

    public void Send(TelegramUserModel userModel, string message)
    {
        _telegramService.Send(userModel, message);
    }

    public void Send(TelegramUserModel[] userModel, string message)
    {
        foreach (TelegramUserModel telegramUserModel in userModel)
        {
            _telegramService.Send(telegramUserModel, message);
        }
    }
}