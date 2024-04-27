using DevMate.Application.Models.Domain;

namespace DevMate.Application.Abstractions.Services;

public interface IEventPublisher
{
    void PostEvent(long recipient, Event eventObject);
}