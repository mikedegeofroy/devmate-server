using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Telegram;

namespace DevMate.Application.Contracts;

public interface IAuthService
{
    AuthResult Login();

    AuthResult VerifyLogin(string? code);

    Task<AuthResult> ApproveLogin(string? code, TelegramUserDto userDto);
}