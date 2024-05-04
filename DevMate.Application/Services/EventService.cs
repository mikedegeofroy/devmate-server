using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Services;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Events;

namespace DevMate.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFileSystem _fileSystem;
    private readonly IEventPublisher _eventPublisher;

    public EventService(IEventRepository eventRepository, IFileSystem fileSystem, IEventPublisher eventPublisher,
        IUserRepository userRepository)
    {
        _eventRepository = eventRepository;
        _fileSystem = fileSystem;
        _eventPublisher = eventPublisher;
        _userRepository = userRepository;
    }

    public EventDto? GetEventById(long id)
    {
        Event? found = _eventRepository.GetEventById(id);

        return found != null
            ? new EventDto
            {
                Id = found.Id,
                Cover = found.Cover,
                Description = found.Description,
                EndDateTime = found.EndDateTime,
                StartDateTime = found.StartDateTime,
                Occupied = found.Occupied,
                Places = found.Places,
                Price = found.Price,
                Title = found.Title,
                UserTelegramId = found.UserTelegramId
            }
            : throw new Exception("Event not found.");
    }

    public IEnumerable<EventDto> GetEvents()
    {
        return _eventRepository.GetEvents().Select(x => new EventDto
        {
            Id = x.Id,
            Cover = x.Cover,
            Description = x.Description,
            EndDateTime = x.EndDateTime,
            StartDateTime = x.StartDateTime,
            Occupied = x.Occupied,
            Places = x.Places,
            Price = x.Price,
            Title = x.Title,
            UserTelegramId = x.UserTelegramId
        });
    }

    public EventDto CreateEvent(long userId)
    {
        User? user = _userRepository.GetUserById(userId);
        if (user == null) throw new Exception("User not found!");

        Event createdEvent = _eventRepository.AddEvent(user);

        return new EventDto
        {
            Id = createdEvent.Id,
            Cover = createdEvent.Cover,
            Description = createdEvent.Description,
            EndDateTime = createdEvent.EndDateTime,
            StartDateTime = createdEvent.StartDateTime,
            Occupied = createdEvent.Occupied,
            Places = createdEvent.Places,
            Price = createdEvent.Price,
            Title = createdEvent.Title,
            UserTelegramId = createdEvent.UserTelegramId
        };
    }

    public EventDto UpdateEvent(EventDto toUpdate)
    {
        Event updatedEvent = _eventRepository.UpdateEvent(toUpdate);

        return new EventDto
        {
            Id = updatedEvent.Id,
            Cover = updatedEvent.Cover,
            Description = updatedEvent.Description,
            EndDateTime = updatedEvent.EndDateTime,
            StartDateTime = updatedEvent.StartDateTime,
            Occupied = updatedEvent.Occupied,
            Places = updatedEvent.Places,
            Price = updatedEvent.Price,
            Title = updatedEvent.Title,
            UserTelegramId = updatedEvent.UserTelegramId
        };
    }

    public EventDto UploadCover(EventDto to, Stream stream)
    {
        string relativePath = Guid.NewGuid() + ".jpg";

        EventDto toUpdate = to with { Cover = _fileSystem.WriteFile(stream, relativePath).GetAwaiter().GetResult() };

        Event updatedEvent = _eventRepository.UpdateEvent(toUpdate);

        return new EventDto
        {
            Id = updatedEvent.Id,
            Cover = updatedEvent.Cover,
            Description = updatedEvent.Description,
            EndDateTime = updatedEvent.EndDateTime,
            StartDateTime = updatedEvent.StartDateTime,
            Occupied = updatedEvent.Occupied,
            Places = updatedEvent.Places,
            Price = updatedEvent.Price,
            Title = updatedEvent.Title,
            UserTelegramId = updatedEvent.UserTelegramId
        };
    }

    public void PostEvent(long id)
    {
        Event? eventById = _eventRepository.GetEventById(id);

        if (eventById != null)
            _eventPublisher.PostEvent(914438292, eventById);
    }

    public void DeleteEvent(long id)
    {
        _eventRepository.DeleteEventById(id);
    }
}