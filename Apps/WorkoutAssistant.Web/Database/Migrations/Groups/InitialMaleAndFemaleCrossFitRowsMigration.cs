using FluentMigrator;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

[Migration(version: 20230704162149)]
public class InitialMaleAndFemaleCrossFitRowsMigration : GroupTableMigration
{
    /// <summary>
    /// Return row values for male crossfit group
    /// </summary>
    private Group MaleCrossFitRow
    {
        get
        {
            return new Group() { Id = Configuration.Rows.MaleCrossFitIndex, Name = "MaleCrossFit" };
        }
    }
    
    /// <summary>
    /// Return row values for female crossfit group
    /// </summary>
    private Group FemaleCrossFitRow
    {
        get
        {
            return new Group() { Id = Configuration.Rows.FemaleCrossFitIndex, Name = "FemaleCrossFit" };
        }
    }
    
    public override void Up()
    {
        Insert.IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MaleCrossFitRow);
        
        Insert.IntoTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: FemaleCrossFitRow);
    }

    public override void Down()
    {
        Delete.FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: FemaleCrossFitRow);
        
        Delete.FromTable(tableName: Configuration.TableName)
            .Row(dataAsAnonymousType: MaleCrossFitRow);
    }
}