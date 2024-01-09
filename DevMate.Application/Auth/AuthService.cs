using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Contracts.Auth;

namespace DevMate.Application.Auth;

public class AuthService : IAuthService
{
    private readonly Dictionary<string, string> _tokens = new();

    public void Login(string username)
    {
        _tokens[username] = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    public bool Verification(string verification)
    {
        throw new NotImplementedException();
    }

    public bool Validate(string username, string token)
    {
        return _tokens[username] == token;
    }
}