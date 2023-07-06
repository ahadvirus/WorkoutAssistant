using WorkoutAssistant.Web.Infrastructures.Database.EntitiesPrimaryIndex;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations;

public class TablePrimaryIndexes
{
    public GroupIndexes Group
    {
        get
        {
            return new GroupIndexes();
        }
    }
}