using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

[Migration(version: 20230706101344)]
public class InitialUsernameIndexMigration : UserTableMigration
{
    public override void Up()
    {
        Create
            .Index(indexName: Configuration.IndexesName.UsernameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Username)
            .Unique();
    }

    public override void Down()
    {
        Delete
            .Index(indexName: Configuration.IndexesName.UsernameUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.Username);
    }
}