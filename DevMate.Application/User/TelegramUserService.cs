using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.User;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.User;

public class TelegramUserService : IUserService
{
    private readonly ITelegramClientService _telegramClientService;
    private readonly IUserRepository _userRepository;

    public TelegramUserService(ITelegramClientService telegramClientService,
        IUserRepository userRepository)
    {
        _telegramClientService = telegramClientService;
        _userRepository = userRepository;
    }

    public async Task<TelegramUserModel> GetUser(UserDto user)
    {
        Stream store = _userRepository.GetStore(user.Phone);
        return await _telegramClientService.GetUser(user.Phone, store);
    }
}