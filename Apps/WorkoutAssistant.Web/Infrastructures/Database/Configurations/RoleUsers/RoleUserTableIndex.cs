using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.RoleUsers;

public class RoleUserTableIndex : TableIndexes<RoleUserTableColumns,
    RoleTable, RoleTableColumns, RoleTableIndexes, RoleTableRows,
    UserTable, UserTableColumns, UserTableIndexes, UserTableRows>
{
    public RoleUserTableIndex(string tableName, RoleUserTableColumns columnsName, RoleTable foreign, UserTable foreignTwo) : base(tableName, columnsName, foreign, foreignTwo)
    {
    }

    public string RoleIdForeign
    {
        get
        {
            return string.Format(format: "FK_{0}_{1}_{2}_{3}", args: new object?[] { ColumnsName.RoleId, TableName, Foreign.ColumnsName.Primary, Foreign.TableName  });
        }
    }
    
    public string UserIdForeign
    {
        get
        {
            return string.Format(format: "FK_{0}_{1}_{2}_{3}", args: new object?[] { ColumnsName.UserId, TableName, ForeignTwo.ColumnsName.Primary, ForeignTwo.TableName  });
        }
    }
    
    public string RoleIdUserIdUnique
    {
        get
        {
            return string.Format(format: "FK_{0}_{1}_{2}", args: new object?[] { ColumnsName.RoleId, ColumnsName.UserId, TableName });
        }
    }
}