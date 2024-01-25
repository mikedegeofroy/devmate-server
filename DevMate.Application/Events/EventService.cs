using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Event;

namespace DevMate.Application.Events;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IFileSystem _fileSystem;

    public EventService(IEventRepository eventRepository, IFileSystem fileSystem)
    {
        _eventRepository = eventRepository;
        _fileSystem = fileSystem;
    }

    public Event? GetEventById(long id)
    {
        return _eventRepository.GetEventById(id);
    }

    public IEnumerable<Event> GetEvents()
    {
        return _eventRepository.GetEvents();
    }

    public Event CreateEvent(Event newEvent)
    {
        return _eventRepository.AddEvent(newEvent);
    }

    public Event UpdateEvent(Event toUpdate)
    {
        return _eventRepository.UpdateEvent(toUpdate);
    }

    public Event UploadCover(Event to, Stream stream)
    {
        string relativePath = "/uploads/" + Guid.NewGuid() + ".jpg";
        var toUpdate = new Event(to.Id, to.UserId, to.Title, to.Description, to.StartDateTime, to.EndDateTime, to.Total,
            to.Occupied, to.Price, _fileSystem.WriteFile(stream, relativePath).GetAwaiter().GetResult());
        return _eventRepository.UpdateEvent(toUpdate);
    }
}