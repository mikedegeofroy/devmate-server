using DevMate.Application.Models.Domain;

namespace DevMate.Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? GetUserById(long id);
    IEnumerable<User> GetUsers();

    User AddUser(User user);

    User UpdateUser(User user);
}