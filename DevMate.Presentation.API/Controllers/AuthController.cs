using DevMate.Application.Contracts.Auth;
using DevMate.Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<AuthResult> Login()
    {
        AuthResult result = _authService.Login();

        return result switch
        {
            AuthResult.NotFound notFound => Ok(notFound),
            AuthResult.VerifyCode verifyCode => Ok(verifyCode),
            AuthResult.Success success => Ok(success),
            _ => NotFound()
        };
    }
    
    [HttpGet("verify")]
    public ActionResult<AuthResult> VerifyLogin(string code)
    {
        AuthResult result = _authService.VerifyLogin(code);

        return result switch
        {
            AuthResult.NotFound notFound => Ok(notFound),
            AuthResult.VerifyCode verifyCode => Ok(verifyCode),
            AuthResult.Success success => Ok(success),
            _ => NotFound()
        };
    }
}