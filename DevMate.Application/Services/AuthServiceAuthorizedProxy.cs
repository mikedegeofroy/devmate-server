using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Telegram;

namespace DevMate.Application.Services;

public class AuthServiceAuthorizedProxy : IAuthService
{
    private readonly IAuthService _authService;
    private readonly IAuthorizedUsersRepository _authorizedUsersRepository;


    public AuthServiceAuthorizedProxy(IAuthService authService, IAuthorizedUsersRepository authorizedUsersRepository)
    {
        _authService = authService;
        _authorizedUsersRepository = authorizedUsersRepository;
    }

    public AuthResult Login()
    {
        return _authService.Login();
    }

    public AuthResult VerifyLogin(string? code)
    {
        return _authService.VerifyLogin(code);
    }

    public Task<AuthResult> ApproveLogin(string? code, TelegramUserDto userDto)
    {
        IEnumerable<WaitlistUser> users = _authorizedUsersRepository.GetAuthorizedUsers();
        return users.FirstOrDefault(x => x.TelegramUsername == userDto.Username) != null
            ? _authService.ApproveLogin(code, userDto)
            : Task.FromResult<AuthResult>(new AuthResult.NotFound());
    }
}