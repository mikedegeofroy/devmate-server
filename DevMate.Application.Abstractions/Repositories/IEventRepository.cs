using DevMate.Application.Models.Event;

namespace DevMate.Application.Abstractions.Repositories;

public interface IEventRepository
{
    Event? GetEventById(long id);
    IEnumerable<Event> GetEvents();

    Event AddEvent(Event newEvent);

    Event UpdateEvent(Event newEvent);
}