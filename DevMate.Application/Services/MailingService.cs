using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Services;

public class MailingService : IMailingService
{
    private readonly ITelegramUserClientFactory _telegramUserClientFactory;
    private readonly IUserRepository _userRepository;

    public MailingService(IUserRepository userRepository, ITelegramUserClientFactory telegramUserClientFactory)
    {
        _userRepository = userRepository;
        _telegramUserClientFactory = telegramUserClientFactory;
    }

    public void Send(TelegramUserModel userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);

        ITelegramUserClient client = _telegramUserClientFactory.GetClient();
        client.Send(userModel, message, user, store);
    }

    public void Send(IEnumerable<TelegramUserModel> userModel, string message, UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        ITelegramUserClient client = _telegramUserClientFactory.GetClient();
        foreach (TelegramUserModel telegramUserModel in userModel)
        {
            client.Send(telegramUserModel, message, user, store);
        }
    }
}