using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Event;

namespace DevMate.Application.Contracts;

public interface IUserService
{
    UserDto GetUser(long id);

    IEnumerable<EventModel> GetEvents(UserDto userDto);
}