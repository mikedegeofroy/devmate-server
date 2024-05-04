using DevMate.Application.Models.Domain;

namespace DevMate.Application.Abstractions.Repositories;

public interface IWaitlistRepository
{
    void AddUser(WaitlistUser user);

    IEnumerable<WaitlistUser> GetUsers();
}