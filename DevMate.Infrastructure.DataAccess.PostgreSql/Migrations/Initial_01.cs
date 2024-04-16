using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Migrations;

[Migration(1, "Initial")]
public class Initial_01 : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) => 
    """
    create table users
    (
        user_id bigint primary key generated always as identity ,
        user_name text not null
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) => 
    """
    drop table users;
    """;
}