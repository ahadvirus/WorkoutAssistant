using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

[Migration(version: 20230704160154)]
public class InitialGroupTableMigration : GroupTableMigration
{
    public override void Up()
    {
        Create.Table(tableName: Configuration.TableName)
            .WithColumn(name: Configuration.ColumnsName.Primary).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.Name).AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table(tableName: Configuration.TableName);
    }
}