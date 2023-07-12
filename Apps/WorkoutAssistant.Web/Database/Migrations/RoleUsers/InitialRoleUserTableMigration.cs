using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.RoleUsers;

[Migration(version: 20230712050345)]
public class InitialRoleUserTableMigration : RoleUserTableMigration
{
    public override void Up()
    {
        Create
            .Table(tableName: Configuration.TableName)
            .WithColumn(name: Configuration.ColumnsName.Primary).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.RoleId).AsGuid().NotNullable()
            .WithColumn(name: Configuration.ColumnsName.UserId).AsGuid().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table(tableName: Configuration.TableName);
    }
}