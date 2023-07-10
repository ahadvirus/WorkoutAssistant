using FluentMigrator;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

[Migration(version: 20230707060822)]
public class InitialDefaultRoleRowsMigration : RoleTableMigration
{
    /// <summary>
    /// Admin row default data in table 
    /// </summary>
    protected Role AdminRow
    {
        get
        {
            return new Role() { Id = Configuration.Rows.AdminId, Name = "Admin" };
        }
    }
    
    /// <summary>
    /// Coach row default data in table 
    /// </summary>
    protected Role CoachRow
    {
        get
        {
            return new Role() { Id = Configuration.Rows.CoachId, Name = "Coach" };
        }
    }
    
    /// <summary>
    /// Trainer row default data in table 
    /// </summary>
    protected Role TrainerRow
    {
        get
        {
            return new Role() { Id = Configuration.Rows.TrainerId, Name = "Trainer" };
        }
    }
    
    public override void Up()
    {
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AdminRow);
        
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: CoachRow);
        
        Insert
            .IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: TrainerRow);
        
    }

    public override void Down()
    {
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: TrainerRow);
        
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: CoachRow);
        
        Delete
            .FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: AdminRow);
    }
}