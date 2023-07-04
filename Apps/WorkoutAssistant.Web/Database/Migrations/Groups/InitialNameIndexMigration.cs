using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

[Migration(version: 20230704160701)]
public class InitialNameIndexMigration : GroupTableMigration
{
    public override void Up()
    {
        Create.Index(indexName: NameIndexName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: NameColumnName)
            .Unique();
    }

    public override void Down()
    {
        Delete.Index(indexName: NameIndexName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: NameColumnName);
    }
}