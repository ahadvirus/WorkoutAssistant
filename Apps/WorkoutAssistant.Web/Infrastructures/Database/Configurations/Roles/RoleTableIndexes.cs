using WorkoutAssistant.Web.Infrastructures.Database.Contracts;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;

public class RoleTableIndexes : TableIndexes<RoleTableColumns>
{
    public string NameUnique
    {
        get
        {
            return string.Format(format: "IX_{0}_{1}", args: new object?[] { TableName, ColumnsName.Name });
        }
    }

    public RoleTableIndexes(string tableName, RoleTableColumns columnsName) : base(tableName, columnsName)
    {
    }
}