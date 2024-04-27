namespace DevMate.Application.Models.Domain;

public record Event
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public long UserTelegramId { get; init; }
    public string Title { get; init; }

    public string Description { get; init; }
    public DateTime? StartDateTime { get; init; }
    public DateTime? EndDateTime { get; init; }
    public long Places { get; init; }
    public long Occupied { get; init; }
    public double Price { get; init; }
    public string? Cover { get; init; }
};