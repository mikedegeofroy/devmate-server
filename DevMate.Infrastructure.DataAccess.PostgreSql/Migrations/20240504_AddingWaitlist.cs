using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Migrations;

[Migration(202405030018)]
public class AddingWaitlist : Migration {
    public override void Up()
    {
        Create.Table("waitlist")
            .WithColumn("telegram_username")
            .AsString()
            .Unique()
            .PrimaryKey();
    }

    public override void Down()
    {
        Delete.Table("waitlist");
    }
}