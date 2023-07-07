using WorkoutAssistant.Web.Infrastructures.Database.Migrations.EntitiesPrimaryIndex;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations;

public class TablePrimaryIndexes
{
    /// <summary>
    /// Represent all indexes use in group at migration
    /// </summary>
    public GroupIndexes Group
    {
        get
        {
            return new GroupIndexes();
        }
    }

    /// <summary>
    /// Represent all indexes for users use in migration
    /// </summary>
    public UserIndexes User
    {
        get
        {
            return new UserIndexes();
        }
    }
}