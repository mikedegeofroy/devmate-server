using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Domain;

namespace DevMate.Application.Services;

public class WaitlistService : IWaitlistService
{
    private readonly IWaitlistRepository _waitlistRepository;

    public WaitlistService(IWaitlistRepository waitlistRepository)
    {
        _waitlistRepository = waitlistRepository;
    }

    public void AddUser(WaitlistUser user)
    {
        _waitlistRepository.AddUser(user);
    }

    public IEnumerable<WaitlistUser> GetUsers()
    {
        return _waitlistRepository.GetUsers();
    }
}