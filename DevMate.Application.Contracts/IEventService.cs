using DevMate.Application.Models.Events;

namespace DevMate.Application.Contracts;

public interface IEventService
{
    EventDto? GetEventById(long id);
    IEnumerable<EventDto> GetEvents();

    EventDto CreateEvent(long userId);

    EventDto UpdateEvent(EventDto toUpdate);

    EventDto UploadCover(EventDto to, Stream stream);
    void PostEvent(long id);
    void DeleteEvent(long id);
}