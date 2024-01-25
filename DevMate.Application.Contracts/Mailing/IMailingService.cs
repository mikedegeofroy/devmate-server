using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Contracts.Mailing;

public interface IMailingService
{
    void Send(TelegramUserModel userModel, string message, UserDto user);
    void Send(IEnumerable<TelegramUserModel> userModel, string message, UserDto user);
}