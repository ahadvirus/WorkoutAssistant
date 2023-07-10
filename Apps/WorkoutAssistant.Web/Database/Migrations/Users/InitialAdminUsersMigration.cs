using FluentMigrator;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

[Migration(version: 20230706111708)]
public class InitialAdminUsersMigration : UserTableMigration
{
    /// <summary>
    /// Definition for mehdi user row data
    /// </summary>
    protected User MehdiRow
    {
        get
        {
            return new User() { Id = Configuration.Rows.MehdiId, Username = "mehdi", Password = "123@asd" };
        }
    }
    
    /// <summary>
    /// Definition for ahad user row data
    /// </summary>
    protected User AhadRow
    {
        get
        {
            return new User() { Id = Configuration.Rows.AhadId, Username = "ahad", Password = "123@asd" };
        }
    }

    public override void Up()
    {
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MehdiRow);
        
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AhadRow);
    }

    public override void Down()
    {
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AhadRow);
        
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MehdiRow);
    }
}