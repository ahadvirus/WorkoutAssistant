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
            return new Role() { Id = Indexes.Role.AdminId, Name = "Admin" };
        }
    }
    
    /// <summary>
    /// Coach row default data in table 
    /// </summary>
    protected Role CoachRow
    {
        get
        {
            return new Role() { Id = Indexes.Role.CoachId, Name = "Coach" };
        }
    }
    
    /// <summary>
    /// Trainer row default data in table 
    /// </summary>
    protected Role TrainerRow
    {
        get
        {
            return new Role() { Id = Indexes.Role.TrainerId, Name = "Trainer" };
        }
    }
    
    public override void Up()
    {
        Insert
            .IntoTable(tableName: TableName)
            .Row(dataAsAnonymousType: AdminRow);
        
        Insert
            .IntoTable(tableName: TableName)
            .Row(dataAsAnonymousType: CoachRow);
        
        Insert
            .IntoTable(tableName: TableName)
            .Row(dataAsAnonymousType: TrainerRow);
        
    }

    public override void Down()
    {
        Delete
            .FromTable(tableName: TableName)
            .Row(dataAsAnonymousType: TrainerRow);
        
        Delete
            .FromTable(tableName: TableName)
            .Row(dataAsAnonymousType: CoachRow);
        
        Delete
            .FromTable(tableName: TableName)
            .Row(dataAsAnonymousType: AdminRow);
    }
}