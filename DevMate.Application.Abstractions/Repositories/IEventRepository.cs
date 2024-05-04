using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Events;

namespace DevMate.Application.Abstractions.Repositories;

public interface IEventRepository
{
    Event? GetEventById(long id);
    IEnumerable<Event> GetEvents();

    Event AddEvent(User user);

    Event UpdateEvent(EventDto toUpdate);

    void AddAttendance(long eventId, long userId, DateTime registrationDatetime);
}