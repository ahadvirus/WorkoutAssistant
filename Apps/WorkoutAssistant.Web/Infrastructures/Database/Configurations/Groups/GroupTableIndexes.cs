using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Groups;

public class GroupTableIndexes : TableIndexes<GroupTableColumns>
{
    public GroupTableIndexes(string tableName, GroupTableColumns columnsName) : base(tableName, columnsName)
    {
    }

    public string NameUnique
    {
        get
        {
            return string.Format(format: "IX_{0}_{1}", args: new object?[] { ColumnsName.Name, TableName });
        }
    }
}