namespace DevMate.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Stream GetStore(string username);
}