using DevMate.Application.Contracts.Auth;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("/api/auth/")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<AuthResult> Login(string phone)
    {
        AuthResult result = _authService.Login(phone);

        return result switch
        {
            AuthResult.InvalidCode invalidCode => Ok(invalidCode),
            AuthResult.InvalidPassword invalidPassword => Ok(invalidPassword),
            AuthResult.NotFound notFound => Ok(notFound),
            AuthResult.RequestCode requestCode => Ok(requestCode),
            AuthResult.RequestPassword requestPassword => Ok(requestPassword),
            AuthResult.Success success => Ok(success),
            _ => NotFound()
        };
    }

    [HttpPost("verify/code")]
    public ActionResult<AuthResult> VerifyCode(Application.Models.Auth.User user)
    {
        AuthResult result = _authService.VerifyLoginCode(user.Phone, user.Secret).GetAwaiter().GetResult();

        return result switch
        {
            AuthResult.InvalidCode invalidCode => Ok(invalidCode),
            AuthResult.InvalidPassword invalidPassword => Ok(invalidPassword),
            AuthResult.NotFound notFound => Ok(notFound),
            AuthResult.RequestCode requestCode => Ok(requestCode),
            AuthResult.RequestPassword requestPassword => Ok(requestPassword),
            AuthResult.Success success => Ok(success),
            _ => NotFound()
        };
    }

    [HttpPost("verify/password")]
    public ActionResult<AuthResult> VerifyPassword(Application.Models.Auth.User user)
    {
        AuthResult result = _authService.VerifyPassword(user.Phone, user.Secret).GetAwaiter().GetResult();

        return result switch
        {
            AuthResult.InvalidCode invalidCode => Ok(invalidCode),
            AuthResult.InvalidPassword invalidPassword => Ok(invalidPassword),
            AuthResult.NotFound notFound => Ok(notFound),
            AuthResult.RequestCode requestCode => Ok(requestCode),
            AuthResult.RequestPassword requestPassword => Ok(requestPassword),
            AuthResult.Success success => Ok(success),
            _ => NotFound()
        };
    }
}