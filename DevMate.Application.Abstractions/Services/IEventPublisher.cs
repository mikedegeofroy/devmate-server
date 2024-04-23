using DevMate.Application.Models.Event;

namespace DevMate.Application.Abstractions.Services;

public interface IEventPublisher
{
    void PostEvent(long recipient, EventModel eventModelObject);
}