using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;
using WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.RoleUsers;

public class RoleUserTable : ITable<RoleUserTableColumns, RoleUserTableIndex, RoleUserTableRows,
    RoleTable, RoleTableColumns, RoleTableIndexes, RoleTableRows,
    UserTable, UserTableColumns, UserTableIndexes, UserTableRows>
{
    public string TableName
    {
        get { return "RoleUsers"; }
    }

    public RoleUserTableColumns ColumnsName
    {
        get { return new RoleUserTableColumns(); }
    }

    public RoleUserTableIndex IndexesName
    {
        get
        {
            return new RoleUserTableIndex(tableName: TableName,
                columnsName: ColumnsName,
                foreign: Foreign,
                foreignTwo: ForeignTwo);
        }
    }

    public RoleUserTableRows Rows
    {
        get { return new RoleUserTableRows(); }
    }

    public RoleTable Foreign
    {
        get { return new RoleTable(); }
    }

    public UserTable ForeignTwo
    {
        get { return new UserTable(); }
    }
}