namespace DevMate.Application.Models.Auth;

public record AuthUserDto
{
    public long Id { get; init; }
    public string ProfilePicture { get; init; }
    public string Username { get; init; }
};