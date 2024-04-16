using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Contracts.User;

public interface IUserService
{
    Task<TelegramUserModel> GetUser(UserDto user);
}