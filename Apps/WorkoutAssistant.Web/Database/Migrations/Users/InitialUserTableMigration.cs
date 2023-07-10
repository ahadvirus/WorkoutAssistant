using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

[Migration(version: 20230706100911)]
public class InitialUserTableMigration : UserTableMigration
{
    public override void Up()
    {
        Create
            .Table(tableName: Configuration.TableName)
            .WithColumn(name: Configuration.ColumnsName.Primary).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.Username).AsString().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.Password).AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table(tableName: Configuration.TableName);
    }
}