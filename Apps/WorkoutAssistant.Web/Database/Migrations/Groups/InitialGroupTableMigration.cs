using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

[Migration(version: 20230704160154)]
public class InitialGroupTableMigration : GroupTableMigration
{
    public override void Up()
    {
        Create.Table(tableName: TableName)
            .WithColumn(name: "Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: NameColumnName).AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table(tableName: TableName);
    }
}