using DevMate.Application.Contracts.Auth;
using DevMate.Application.Models.Auth;

namespace DevMate.Application.Services.Auth;

public class TelegramAuthService : IAuthService
{
    public AuthResult Login(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResult> VerifyLoginCode(string phone, string code)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResult> VerifyPassword(string phone, string password)
    {
        throw new NotImplementedException();
    }
}