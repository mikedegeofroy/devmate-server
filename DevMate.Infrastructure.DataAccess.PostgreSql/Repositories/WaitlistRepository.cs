using System.Data;
using Dapper;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Domain;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class WaitlistRepository : IWaitlistRepository
{
    private readonly SqlDataAccess _sql;

    public WaitlistRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }

    public void AddUser(WaitlistUser user)
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                INSERT INTO waitlist (telegram_username) VALUES (@TelegramUsername)
                             """;

        connection.Execute(query, new
        {
            user.TelegramUsername
        });
    }

    public IEnumerable<WaitlistUser> GetUsers()
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT telegram_username AS TelegramUsername FROM waitlist;
                             """;

        return connection.Query<WaitlistUser>(query).ToList();
    }
}