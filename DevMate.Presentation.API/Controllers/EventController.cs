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
    [HttpPut("update")]
    public ActionResult<EventDto> UpdateEvent(EventDto update)
    {
        return Ok(_eventService.UpdateEvent(update));
    }

    [Authorize]
    [HttpPost("create")]
    public ActionResult<Event> CreateEvent()
    {
        string? userId = User.FindFirst(ClaimTypes.Sid)?.Value;
        
        Console.WriteLine(string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));

        
        if (userId == null)
        {
            return Unauthorized("User ID is not found in the token");
        }

        return Ok(_eventService.CreateEvent(long.Parse(userId)));
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