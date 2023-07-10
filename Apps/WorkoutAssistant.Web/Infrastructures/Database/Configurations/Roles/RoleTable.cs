using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;

public class RoleTable : ITable<RoleTableColumns, RoleTableIndexes, RoleTableRows>
{
    public string TableName
    {
        get { return "Roles"; }
    }

    public RoleTableColumns ColumnsName
    {
        get { return new RoleTableColumns(); }
    }

    public RoleTableIndexes IndexesName
    {
        get { return new RoleTableIndexes(tableName: TableName, columnsName: ColumnsName); }
    }

    public RoleTableRows Rows
    {
        get { return new RoleTableRows(); }
    }
}