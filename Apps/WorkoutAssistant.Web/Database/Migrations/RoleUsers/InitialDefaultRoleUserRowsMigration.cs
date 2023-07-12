using FluentMigrator;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Migrations.RoleUsers;

[Migration(version: 20230712052520)]
public class InitialDefaultRoleUserRowsMigration : RoleUserTableMigration
{
    protected RoleUser AhadToAdminRow
    {
        get
        {
            return new RoleUser()
            {
                Id = Configuration.Rows.AhadIdToAdminId, 
                RoleId = Configuration.Foreign.Rows.AdminId,
                UserId = Configuration.ForeignTwo.Rows.AhadId
            };
        }
    }
    
    protected RoleUser MehdiToAdminRow
    {
        get
        {
            return new RoleUser()
            {
                Id = Configuration.Rows.MehdiIdToAdminId, 
                RoleId = Configuration.Foreign.Rows.AdminId,
                UserId = Configuration.ForeignTwo.Rows.MehdiId
            };
        }
    }
    
    public override void Up()
    {
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AhadToAdminRow);
        
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MehdiToAdminRow);
    }

    public override void Down()
    {
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MehdiToAdminRow);

        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AhadToAdminRow);
    }
}