namespace DevMate.Application.Models.Events;

public record EventDto
{
    public long Id { get; init; }
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