using ParkingApp.Application.Abstractions.Telegram.Services;
using ParkingApp.Application.Contracts.Analytics;
using ParkingApp.Application.Models;

namespace ParkingApp.Application.MailingService;

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