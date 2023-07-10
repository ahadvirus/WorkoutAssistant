using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;

public class UserTable : ITable<UserTableColumns, UserTableIndexes, UserTableRows>
{
    public string TableName
    {
        get { return "Users"; }
    }

    public UserTableColumns ColumnsName
    {
        get { return new UserTableColumns(); }
    }

    public UserTableIndexes IndexesName
    {
        get { return new UserTableIndexes(tableName: TableName, columnsName: ColumnsName); }
    }

    public UserTableRows Rows
    {
        get { return new UserTableRows(); }
    }
}