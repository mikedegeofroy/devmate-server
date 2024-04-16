using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.MailingService;

public class MailingService : IMailingService
{
    private readonly ITelegramUserClient _telegramUserClient;
    private readonly IUserRepository _userRepository;

    public MailingService(ITelegramUserClient telegramUserClient, IUserRepository userRepository)
    {
        _telegramUserClient = telegramUserClient;
        _userRepository = userRepository;
    }

    public void Send(TelegramUserModel userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        _telegramUserClient.Send(userModel, message, user, store);
    }

    public void Send(IEnumerable<TelegramUserModel> userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        foreach (TelegramUserModel telegramUserModel in userModel)
        {
            _telegramUserClient.Send(telegramUserModel, message, user, store);
        }
    }
}