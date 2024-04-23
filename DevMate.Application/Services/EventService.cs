using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Services;
using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Event;

namespace DevMate.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IFileSystem _fileSystem;
    private readonly IEventPublisher _eventPublisher;

    public EventService(IEventRepository eventRepository, IFileSystem fileSystem, IEventPublisher eventPublisher)
    {
        _eventRepository = eventRepository;
        _fileSystem = fileSystem;
        _eventPublisher = eventPublisher;
    }

    public EventModel? GetEventById(long id)
    {
        return _eventRepository.GetEventById(id);
    }

    public IEnumerable<EventModel> GetEvents()
    {
        return _eventRepository.GetEvents();
    }

    public EventModel CreateEvent()
    {
        return _eventRepository.AddEvent();
    }

    public EventModel UpdateEvent(EventModel toUpdate)
    {
        return _eventRepository.UpdateEvent(toUpdate);
    }

    public EventModel UploadCover(EventModel to, Stream stream)
    {
        string relativePath = "/uploads/" + Guid.NewGuid() + ".jpg";
        EventModel toUpdate = to with { Cover = _fileSystem.WriteFile(stream, relativePath).GetAwaiter().GetResult() };
        return _eventRepository.UpdateEvent(toUpdate);
    }

    public void PostEvent(long id)
    {
        EventModel? eventById = _eventRepository.GetEventById(id);

        if (eventById != null)
            _eventPublisher.PostEvent(914438292, eventById);
    }
}