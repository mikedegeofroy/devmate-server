using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Events;

namespace DevMate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;

    public UserService(IUserRepository userRepository, IEventRepository eventRepository)
    {
        _userRepository = userRepository;
        _eventRepository = eventRepository;
    }

    public AuthUserDto GetUser(long id)
    {
        User? user = _userRepository.GetUserById(id);
        if (user != null)
            return new AuthUserDto
            {
                Id = user.Id,
                ProfilePicture = user.ProfilePicture,
                Username = user.Username
            };
        throw new Exception("User not found");
    }

    public IEnumerable<EventDto> GetEvents(AuthUserDto authUserDto)
    {
        return _eventRepository
            .GetEvents()
            .Where(x => x.UserId == authUserDto.Id)
            .Select(x => new EventDto
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
}