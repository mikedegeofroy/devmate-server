using System.Data;
using Dapper;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Domain;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class AuthorizedUsersRepository : IAuthorizedUsersRepository
{
    private readonly SqlDataAccess _sql;

    public AuthorizedUsersRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }

    public IEnumerable<WaitlistUser> GetAuthorizedUsers()
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT telegram_username AS TelegramUsername FROM authorized_users;
                             """;

        return connection.Query<WaitlistUser>(query).ToList();
    }
}