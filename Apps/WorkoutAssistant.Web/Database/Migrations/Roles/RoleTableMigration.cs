using WorkoutAssistant.Web.Infrastructures.Database.Migrations;

namespace WorkoutAssistant.Web.Database.Migrations.Roles;

public abstract class RoleTableMigration : TableMigration
{
    protected override string TableName
    {
        get
        {
            return "Roles";
        }
    }

    /// <summary>
    /// Represent name for name column
    /// </summary>
    protected string NameColumnName
    {
        get
        {
            return "Name";
        }
    }

    /// <summary>
    /// Represent index name for name column
    /// </summary>
    protected string NameIndexName
    {
        get
        {
            return string.Format(format: "IX_{0}_{1}", args: new object?[] { NameColumnName, TableName });
        }
    }
}