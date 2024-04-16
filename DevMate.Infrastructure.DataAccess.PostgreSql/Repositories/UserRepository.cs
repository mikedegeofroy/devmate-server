using DevMate.Application.Abstractions.Repositories;
using DevMate.Infrastructure.DataAccess.PostgreSql.Stores;
using Itmo.Dev.Platform.Postgres.Connection;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Stream GetStore(string username)
    {
        return new ClientStore(_connectionProvider, username);
    }
}