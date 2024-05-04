using System.Security.Claims;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Events;
using Microsoft.AspNetCore.Authorization;
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
    public ActionResult<IEnumerable<Event>> Events()
    {
        IEnumerable<EventDto> events = _eventService.GetEvents();

        return Ok(events);
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Event> CreateEvent()
    {
        string? userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized("User ID is not found in the token");
        }

        return Ok(_eventService.CreateEvent(long.Parse(userId)));
    }

    [Authorize]
    [HttpPut]
    public ActionResult<EventDto> UpdateEvent(EventDto update)
    {
        return Ok(_eventService.UpdateEvent(update));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<EventDto> DeleteEvent(string id)
    {
        _eventService.DeleteEvent(long.Parse(id));
        return Ok();
    }

    [Authorize]
    [HttpPost("post")]
    public ActionResult<Event> PostEvent(long id)
    {
        _eventService.PostEvent(id);
        return Ok();
    }

    [Authorize]
    [HttpPost("upload-cover")]
    public ActionResult<Event> UploadCover(long id, IFormFile file)
    {
        Console.WriteLine(file.ContentType);
        EventDto? eventById = _eventService.GetEventById(id);
        if (eventById != null)
            return Ok(_eventService.UploadCover(eventById, file.OpenReadStream()));
        return NotFound();
    }
}