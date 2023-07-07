using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

[Migration(version: 20230706101344)]
public class InitialUsernameIndexMigration : UserTableMigration
{
    public override void Up()
    {
        Create
            .Index(indexName: UsernameIndexName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: UsernameColumnName)
            .Unique();
    }

    public override void Down()
    {
        Delete
            .Index(indexName: UsernameIndexName)
            .OnTable(tableName: TableName)
            .OnColumn(columnName: UsernameColumnName);
    }
}