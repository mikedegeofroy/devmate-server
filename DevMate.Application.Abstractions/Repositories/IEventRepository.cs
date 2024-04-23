using DevMate.Application.Models.Event;

namespace DevMate.Application.Abstractions.Repositories;

public interface IEventRepository
{
    EventModel? GetEventById(long id);
    IEnumerable<EventModel> GetEvents();

    EventModel AddEvent();

    EventModel UpdateEvent(EventModel newEventModel);
}