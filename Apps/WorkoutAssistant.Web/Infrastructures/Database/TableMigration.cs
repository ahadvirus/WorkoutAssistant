using FluentMigrator;

namespace WorkoutAssistant.Web.Infrastructures.Database;

public abstract class TableMigration : Migration
{
    /// <summary>
    /// Represent the name of table in database
    /// </summary>
    protected abstract string TableName { get; }

    /// <summary>
    /// Represent all indexes can be use in migrations for all tables
    /// </summary>
    protected TablePrimaryIndexes Indexes
    {
        get
        {
            return new TablePrimaryIndexes();
        }
    }
}