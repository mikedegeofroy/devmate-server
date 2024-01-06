using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Workshop5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) => 
    """
    create table parkings
    (
        parking_id bigint primary key generated always as identity
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) => 
    """
    drop table parkings
    """;
}