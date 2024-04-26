using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Event;

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

    public UserDto GetUser(long id)
    {
        User? user = _userRepository.GetUserById(id);
        if (user != null)
            return new UserDto(user.Id, "", user.Username, "");
        throw new Exception("User not found");
    }

    public IEnumerable<EventModel> GetEvents(UserDto userDto)
    {
        return _eventRepository.GetEvents().Where(x => x.UserId == userDto.Id);
    }
}