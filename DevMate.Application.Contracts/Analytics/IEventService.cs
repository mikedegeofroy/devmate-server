using DevMate.Application.Models.Event;

namespace DevMate.Application.Contracts.Analytics;

public interface IEventService
{
    Event? GetEventById(long id);
    IEnumerable<Event> GetEvents();

    Event CreateEvent(Event newEvent);

    Event UpdateEvent(Event toUpdate);

    Event UploadCover(Event to, Stream stream);
}