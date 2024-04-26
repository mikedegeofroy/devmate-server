namespace DevMate.Application.Models.Auth;

public record User
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public string Username { get; init; }
};