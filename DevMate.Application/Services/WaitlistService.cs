using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;

namespace DevMate.Application.Services;

public class WaitlistService : IWaitlistService
{
    private readonly IWaitlistRepository _waitlistRepository;

    public WaitlistService(IWaitlistRepository waitlistRepository)
    {
        _waitlistRepository = waitlistRepository;
    }

    public void AddUser(string telegramUsername)
    {
        _waitlistRepository.AddUser(telegramUsername);
    }
}