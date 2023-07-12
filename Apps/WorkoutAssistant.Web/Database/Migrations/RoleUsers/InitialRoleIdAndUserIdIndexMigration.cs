using FluentMigrator;

namespace WorkoutAssistant.Web.Database.Migrations.RoleUsers;

[Migration(version: 20230712051201)]
public class InitialRoleIdAndUserIdIndexMigration : RoleUserTableMigration
{
    public override void Up()
    {
        Create
            .ForeignKey(foreignKeyName: Configuration.IndexesName.RoleIdForeign)
            .FromTable(table: Configuration.TableName)
            .ForeignColumn(column: Configuration.ColumnsName.RoleId)
            .ToTable(table: Configuration.Foreign.TableName)
            .PrimaryColumn(column: Configuration.Foreign.ColumnsName.Primary);

        Create
            .ForeignKey(foreignKeyName: Configuration.IndexesName.UserIdForeign)
            .FromTable(table: Configuration.TableName)
            .ForeignColumn(column: Configuration.ColumnsName.UserId)
            .ToTable(table: Configuration.ForeignTwo.TableName)
            .PrimaryColumn(column: Configuration.ForeignTwo.ColumnsName.Primary);

        Create
            .Index(indexName: Configuration.IndexesName.RoleIdUserIdUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumn(columnName: Configuration.ColumnsName.RoleId)
            .Unique()
            .OnColumn(columnName: Configuration.ColumnsName.UserId)
            .Unique();
    }

    public override void Down()
    {
        Delete
            .Index(indexName: Configuration.IndexesName.RoleIdUserIdUnique)
            .OnTable(tableName: Configuration.TableName)
            .OnColumns(columnNames: new string[]
            {
                Configuration.ColumnsName.RoleId,
                Configuration.ColumnsName.UserId
            });

        Delete
            .ForeignKey(foreignKeyName: Configuration.IndexesName.UserIdForeign)
            .OnTable(foreignTableName: Configuration.TableName);


        Delete
            .ForeignKey(foreignKeyName: Configuration.IndexesName.RoleIdForeign)
            .OnTable(foreignTableName: Configuration.TableName);
    }
}