using WorkoutAssistant.Web.Infrastructures.Database;

namespace WorkoutAssistant.Web.Database.Migrations.Groups;

public abstract class GroupTableMigration : TableMigration
{
    protected override string TableName
    {
        get { return "Groups"; }
    }

    /// <summary>
    /// Define column name of name column in table
    /// </summary>
    protected string NameColumnName
    {
        get { return "Name"; }
    }

    /// <summary>
    /// Define column name of name column in table
    /// </summary>
    protected string NameIndexName
    {
        get { return string.Format(format: "IX_{0}_{1}", args: new object?[] { NameColumnName, TableName }); }
    }
}