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

    public void AddUser(string telegramUsername)
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                INSERT INTO waitlist (telegram_username) VALUES (@TelegramUsername)
                             """;

        connection.Execute(query, new
        {
            TelegramUsername = telegramUsername
        });
    }
}