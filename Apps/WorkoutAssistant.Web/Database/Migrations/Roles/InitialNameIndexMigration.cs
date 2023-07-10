using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

[Migration(version: 20230707054249)]
public class InitialNameIndexMigration : RoleTableMigration
{
    public override void Up()
    {
        Create
            .Index(indexName: Configuration.IndexesName.NameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Name)
            .Unique();
    }

    public override void Down()
    {
        Delete
            .Index(indexName: Configuration.IndexesName.NameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Name);
    }
}