using DevMate.Application.Models.Domain;

namespace DevMate.Application.Abstractions.Repositories;

public interface IAuthorizedUsersRepository
{
    IEnumerable<WaitlistUser> GetAuthorizedUsers();
}