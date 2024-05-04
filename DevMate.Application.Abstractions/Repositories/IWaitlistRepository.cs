namespace DevMate.Application.Abstractions.Repositories;

public interface IWaitlistRepository
{
    void AddUser(string telegramUsername);
}