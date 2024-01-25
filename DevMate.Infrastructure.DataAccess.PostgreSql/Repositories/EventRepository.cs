using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Event;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class EventRepository : IEventRepository
{
    private readonly List<Event> _events = new List<Event>
    {
        new Event(0, 0, "Title1", "Description", DateTime.Now, DateTime.Now.AddHours(2),
            10, 5, 200.50,
            ""),
        new Event(1, 0, "Title2", "Description", DateTime.Now, DateTime.Now.AddHours(2),
            10, 5, 200.50,
            ""),
    };

    public Event? GetEventById(long id)
    {
        return _events.Find(e => e.Id == id);
    }

    public IEnumerable<Event> GetEvents()
    {
        return _events;
    }

    public Event AddEvent(Event newEvent)
    {
        _events.Add(newEvent);
        return newEvent;
    }

    public Event UpdateEvent(Event newEvent)
    {
        int index = _events.FindIndex(e => e.Id == newEvent.Id);
        if (index > -1)
            _events[index] = newEvent;
        return newEvent;
    }
}