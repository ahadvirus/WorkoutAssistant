using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

[Migration(version: 20230707054249)]
public class InitialNameIndexMigration : RoleTableMigration
{
    public override void Up()
    {
        Create
            .Index(indexName: NameColumnName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: NameColumnName)
            .Unique();
    }

    public override void Down()
    {
        Delete
            .Index(indexName: NameIndexName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: NameColumnName);
    }
}