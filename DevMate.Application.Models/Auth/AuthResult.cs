namespace DevMate.Application.Models.Auth;

public record AuthResult
{
    public string Message { get; private set; }

    private AuthResult(string message)
    {
        Message = message;
    }

    public sealed record Success(UserDto User) : AuthResult("Welcome!");

    public sealed record RequestCode() : AuthResult("We've sent your verification code to your telegram");

    public sealed record RequestPassword() : AuthResult("Please enter your telegram password");

    public sealed record InvalidCode() : AuthResult("The code is invalid, please try again.");

    public sealed record InvalidPassword() : AuthResult("The password is invalid, please try again.");

    public sealed record NotFound() : AuthResult("No user found.");
}