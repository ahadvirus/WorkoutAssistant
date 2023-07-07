using WorkoutAssistant.Web.Infrastructures.Database.Migrations;

namespace WorkoutAssistant.Web.Database.Migrations.Users;

public abstract class UserTableMigration : TableMigration
{

    /// <summary>
    /// Define username column's name in table
    /// </summary>
    protected string UsernameColumnName
    {
        get
        {
            return "Username";
        }
    }

    /// <summary>
    /// Represent index name for username column
    /// </summary>
    protected string UsernameIndexName
    {
        get
        {
            return string.Format(format: "IX_{0}_{1}", args: new object?[] { UsernameColumnName, TableName });
        }
    }

    protected override string TableName
    {
        get
        {
            return "Users";
        }
    }
}