using DevMate.Application.Models.Domain;

namespace DevMate.Application.Contracts;

public interface IWaitlistService
{
    void AddUser(WaitlistUser user);

    IEnumerable<WaitlistUser> GetUsers();
}