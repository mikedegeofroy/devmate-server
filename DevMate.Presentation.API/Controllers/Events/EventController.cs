using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers.Events;

[ApiController]
[Route("/api/events/")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Event>> Events()
    {
        IEnumerable<Event> events = _eventService.GetEvents();

        return Ok(events);
    }

    [HttpPut("update")]
    public ActionResult<Event> UpdateEvent(Event update)
    {
        return Ok(_eventService.UpdateEvent(update));
    }

    [HttpPost("create")]
    public ActionResult<Event> CreateEvent(Event create)
    {
        return Ok(_eventService.CreateEvent(create));
    }

    [HttpPost("upload-cover")]
    public ActionResult<Event> UploadCover(long id, IFormFile file)
    {
        Console.WriteLine(file.ContentType);
        Event? eventById = _eventService.GetEventById(id);
        if (eventById != null)
            return Ok(_eventService.UploadCover(eventById, file.OpenReadStream()));
        return NotFound();
    }
}