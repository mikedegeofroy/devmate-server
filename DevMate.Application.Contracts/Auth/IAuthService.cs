namespace DevMate.Application.Contracts.Auth;

public interface IAuthService
{
    void Login(string username);
    bool Verification(string verification);

    bool Validate(string username, string token);
}