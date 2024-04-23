using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Event;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class EventRepository : IEventRepository
{
    private readonly List<EventModel> _events = new List<EventModel>
    {
        new EventModel
        {
            Id = 0,
            UserId = 0,
            Title = "This is a title 1",
            Description = "This is some description",
            StartDateTime = new DateTime(),
            EndDateTime = new DateTime().AddHours(2),
            Total = 0,
            Occupied = 0,
            Price = 0,
            Cover = "null"
        },
        new()
        {
            Id = 1,
            UserId = 0,
            Title = "This is a title 2",
            Description = "This is some description",
            StartDateTime = new DateTime(),
            EndDateTime = new DateTime().AddHours(2),
            Total = 0,
            Occupied = 0,
            Price = 0,
            Cover = "null"
        },
    };

    public EventModel? GetEventById(long id)
    {
        return _events.Find(e => e.Id == id);
    }

    public IEnumerable<EventModel> GetEvents()
    {
        return _events;
    }

    public EventModel AddEvent()
    {
        var newEvent = new EventModel
        {
            Id = 0,
            UserId = 0,
            Title = "This is a title",
            Description = "This is some description",
            StartDateTime = new DateTime(),
            EndDateTime = new DateTime().AddHours(2),
            Total = 0,
            Occupied = 0,
            Price = 0,
            Cover = "null"
        };

        _events.Add(newEvent);
        return newEvent;
    }

    public EventModel UpdateEvent(EventModel newEventModel)
    {
        int index = _events.FindIndex(e => e.Id == newEventModel.Id);
        if (index > -1)
            _events[index] = newEventModel;
        return newEventModel;
    }
}