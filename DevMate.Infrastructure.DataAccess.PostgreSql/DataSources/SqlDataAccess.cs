using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

public class SqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string GetConnectionString()
    {
        return _configuration.GetSection("AppSettings:DatabaseConnectionString").Value ??
               throw new Exception("No connection string found.");
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(GetConnectionString());
    }
}