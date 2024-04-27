namespace DevMate.Application.Models.Domain;

public record User
{
    public long Id { get; init; }
    public long TelegramId { get; init; }
    public string Username { get; init; }
    
    public string ProfilePicture { get; init; }
}