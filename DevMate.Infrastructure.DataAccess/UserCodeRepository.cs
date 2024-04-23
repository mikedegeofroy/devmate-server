using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Auth;

namespace DevMate.Infrastructure.DataAccess;

public class UserCodeRepository : IUserCodeRepository
{
    private readonly Dictionary<string?, User> _users = new();

    public void RegisterUserCode(string? code, User user, int ttl)
    {
        _users[code] = user;
        Task.Run(() =>
        {
            Thread.Sleep(ttl * 1000);
            _users.Remove(code);
        });
    }

    public User? GetUserByCode(string? code)
    {
        return _users.GetValueOrDefault(code);
    }
}