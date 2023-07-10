using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Groups;

public class GroupTable : ITable<GroupTableColumns, GroupTableIndexes, GroupTableRows>
{
    public string TableName
    {
        get { return "Groups"; }
    }


    public GroupTableIndexes IndexesName
    {
        get { return new GroupTableIndexes(tableName: TableName, columnsName: ColumnsName); }
    }

    public GroupTableRows Rows
    {
        get { return new GroupTableRows(); }
    }

    public GroupTableColumns ColumnsName
    {
        get { return new GroupTableColumns(); }
    }
}