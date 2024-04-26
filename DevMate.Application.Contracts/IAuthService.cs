using DevMate.Application.Models.Auth;

namespace DevMate.Application.Contracts;

public interface IAuthService
{
    AuthResult Login();

    AuthResult VerifyLogin(string? code);

    AuthResult ApproveLogin(string? code, Models.Auth.User user);
}