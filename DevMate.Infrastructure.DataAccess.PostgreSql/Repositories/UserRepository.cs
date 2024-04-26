using System.Data;
using Dapper;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Event;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqlDataAccess _sql;

    public UserRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }

    public User? GetUserById(long id)
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT * FROM users WHERE id = @Id;
                             """;

        User? user = connection.QueryFirstOrDefault<User>(query, new { Id = id });

        return user;
    }

    public IEnumerable<User> GetUsers()
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT id, user_id as UserId, username FROM users;
                             """;

        var users = connection.Query<User>(query)
            .Distinct()
            .ToList();

        return users;
    }

    public User AddUser(User user)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          INSERT INTO users (user_id, username)
                                          VALUES (@UserId, @Username)
                                          RETURNING *
                                      """;

        User? insertedUser = connection.QueryFirstOrDefault<User>(permalinkQuery, new
        {
            user.UserId,
            user.Username
        });

        return insertedUser;
    }

    public User UpdateUser(User user)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          UPDATE users SET
                                            username = @Username
                                                        WHERE user_id = @Id
                                          RETURNING *
                                      """;

        User? updatedUser = connection.ExecuteScalar<User>(permalinkQuery, new
        {
            user.Username
        });

        return updatedUser;
    }
}