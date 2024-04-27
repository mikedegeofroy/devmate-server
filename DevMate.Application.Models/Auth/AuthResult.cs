namespace DevMate.Application.Models.Auth;

public record AuthResult
{
    public string Message { get; private set; }

    private AuthResult(string message)
    {
        Message = message;
    }

    public sealed record Success(AuthUserDto User, string Secret) : AuthResult("Welcome!");

    public sealed record VerifyCode(string Code) : AuthResult("Verify through telegram bot");

    public sealed record NotFound() : AuthResult("No user found.");
}