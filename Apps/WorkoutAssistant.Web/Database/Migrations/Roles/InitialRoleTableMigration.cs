using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

[Migration(version: 20230707053948)]
public class InitialRoleTableMigration : RoleTableMigration
{
    public override void Up()
    {
        Create
            .Table(tableName: TableName)
            .WithColumn(name: "Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: NameColumnName).AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table(tableName: TableName);
    }
}