using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Oracle;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Migrations;

[Migration(202505040519)]
public class AddingAuthorizedUsers : Migration {
    public override void Up()
    {
        Create.Table("authorized_users")
            .WithColumn("telegram_username").AsString().Unique();

        Insert.IntoTable("authorized_users").Row(new { telegram_username = "seven_fridays" });
        Insert.IntoTable("authorized_users").Row(new { telegram_username = "mikedegeofroy" });
    }

    public override void Down()
    {
        Delete.Table("authorized_users");
    }
}