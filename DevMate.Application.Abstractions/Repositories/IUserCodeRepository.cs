using DevMate.Application.Models.Auth;

namespace DevMate.Application.Abstractions.Repositories;

public interface IUserCodeRepository
{
    void RegisterUserCode(string? code, User user, int ttl);
    User? GetUserByCode(string? code);
}