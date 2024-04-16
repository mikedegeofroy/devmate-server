using DevMate.Application.Models.Auth;

namespace DevMate.Application.Contracts.Auth;

public interface IAuthService
{
    AuthResult Login(string phone);
    
    Task<AuthResult> VerifyLoginCode(string phone, string code);
    
    Task<AuthResult> VerifyPassword(string phone, string password);
}