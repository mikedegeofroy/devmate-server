using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.MailingService;

public class MailingService : IMailingService
{
    private readonly ITelegramClientService _telegramClientService;
    private readonly IUserRepository _userRepository;

    public MailingService(ITelegramClientService telegramClientService, IUserRepository userRepository)
    {
        _telegramClientService = telegramClientService;
        _userRepository = userRepository;
    }

    public void Send(TelegramUserModel userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        _telegramClientService.Send(userModel, message, user, store);
    }

    public void Send(IEnumerable<TelegramUserModel> userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        foreach (TelegramUserModel telegramUserModel in userModel)
        {
            _telegramClientService.Send(telegramUserModel, message, user, store);
        }
    }
}