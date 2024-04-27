using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Events;

namespace DevMate.Application.Contracts;

public interface IUserService
{
    AuthUserDto GetUser(long id);

    IEnumerable<EventDto> GetEvents(AuthUserDto authUserDto);
}