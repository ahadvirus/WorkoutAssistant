using WorkoutAssistant.Web.Infrastructures.Database.Contracts;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;

public class UserTableIndexes : TableIndexes<UserTableColumns>
{
    public UserTableIndexes(string tableName, UserTableColumns columnsName) : base(tableName, columnsName)
    {
    }
    
    /// <summary>
    /// Represent index name for username column
    /// </summary>
    public string UsernameUnique
    {
        get
        {
            return string.Format(format: "IX_{0}_{1}", args: new object?[] { ColumnsName.Username, TableName });
        }
    }
}