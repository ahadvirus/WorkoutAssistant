using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

[Migration(version: 20230707053948)]
public class InitialRoleTableMigration : RoleTableMigration
{
    public override void Up()
    {
        Create
            .Table(tableName: Configuration.TableName)
            .WithColumn(name: Configuration.ColumnsName.Primary).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.Name).AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table(tableName: Configuration.TableName);
    }
}