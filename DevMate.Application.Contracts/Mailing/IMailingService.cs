using DevMate.Application.Models;
using DevMate.Application.Models.Analytics;

namespace DevMate.Application.Contracts.Mailing;

public interface IMailingService
{
    void Send(TelegramUserModel userModel, string message);
    void Send(TelegramUserModel[] userModel, string message);
}