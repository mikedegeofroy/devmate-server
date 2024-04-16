namespace DevMate.Application.Models.Event;

public record Event(
    long Id,
    long UserId,
    string Title,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    long Total,
    long Occupied,
    double Price,
    string Cover);