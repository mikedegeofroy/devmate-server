using DevMate.Application.Models.Auth;

namespace DevMate.Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? GetUserById(long id);
    IEnumerable<User> GetUsers();

    User AddUser(User user);

    User UpdateUser(User user);
}