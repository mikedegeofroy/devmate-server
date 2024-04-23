using DevMate.Application.Contracts.Analytics;
using DevMate.Application.Models.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevMate.Presentation.API.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventModel>> Events()
    {
        IEnumerable<EventModel> events = _eventService.GetEvents();

        return Ok(events);
    }

    [HttpPut("update")]
    public ActionResult<EventModel> UpdateEvent(EventModel update)
    {
        return Ok(_eventService.UpdateEvent(update));
    }

    [HttpPost("create")]
    public ActionResult<EventModel> CreateEvent()
    {
        return Ok(_eventService.CreateEvent());
    }
    
    [HttpPost("post")]
    public ActionResult<EventModel> PostEvent(long id)
    {
        _eventService.PostEvent(id);
        return Ok();
    }

    [HttpPost("upload-cover")]
    public ActionResult<EventModel> UploadCover(long id, IFormFile file)
    {
        Console.WriteLine(file.ContentType);
        EventModel? eventById = _eventService.GetEventById(id);
        if (eventById != null)
            return Ok(_eventService.UploadCover(eventById, file.OpenReadStream()));
        return NotFound();
    }
}