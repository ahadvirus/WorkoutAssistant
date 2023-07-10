using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

[Migration(version: 20230704160701)]
public class InitialNameIndexMigration : GroupTableMigration
{
    public override void Up()
    {
        Create.Index(indexName: Configuration.IndexesName.NameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Name)
            .Unique();
    }

    public override void Down()
    {
        Delete.Index(indexName: Configuration.IndexesName.NameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Name);
    }
}