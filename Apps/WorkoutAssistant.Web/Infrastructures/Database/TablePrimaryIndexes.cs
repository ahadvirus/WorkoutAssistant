using WorkoutAssistant.Web.Infrastructures.Database.EntitiesPrimaryIndex;

namespace WorkoutAssistant.Web.Infrastructures.Database;

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