using DevMate.Application.Models.Event;

namespace DevMate.Application.Contracts;

public interface IEventService
{
    EventModel? GetEventById(long id);
    IEnumerable<EventModel> GetEvents();

    EventModel CreateEvent(long userId);

    EventModel UpdateEvent(EventModel toUpdate);

    EventModel UploadCover(EventModel to, Stream stream);
    void PostEvent(long id);
}