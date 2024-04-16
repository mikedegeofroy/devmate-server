using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using DevMate.Application.Contracts.User;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.User;

public class TelegramUserService : IUserService
{
    private readonly ITelegramUserClient _telegramUserClient;
    private readonly IUserRepository _userRepository;

    public TelegramUserService(ITelegramUserClient telegramUserClient,
        IUserRepository userRepository)
    {
        _telegramUserClient = telegramUserClient;
        _userRepository = userRepository;
    }

    public Task<TelegramUserModel> GetUser(UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        return Task.FromResult(_telegramUserClient.GetUser(user.Phone, store));
    }
}