namespace DevMate.Application.Models.Telegram;

public record TelegramUserDto
{
    public long TelegramId { get; init; }
    public string Username { get; init; }
}