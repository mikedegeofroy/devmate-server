using FluentMigrator;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Migrations;

[Migration(20240423)]
public class Initial : Migration {
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("user_id").AsInt64().Unique()
            .WithColumn("username").AsString().Unique().PrimaryKey();
        
        Create.Table("events")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("user_id").AsInt64()
            .WithColumn("title").AsString()
            .WithColumn("description").AsString()
            .WithColumn("places").AsInt16()
            .WithColumn("occupied").AsInt16()
            .WithColumn("price").AsFloat()
            .WithColumn("cover").AsString().Nullable()
            .WithColumn("end_datetime").AsDateTime().Nullable()
            .WithColumn("start_datetime").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("events");
    }
}