using FluentMigrator;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations;

public abstract class TableMigration<TTable, TColumns, TIndexes, TRows> : Migration, 
    ITableConfiguration<TTable, TColumns, TIndexes, TRows> 
    where TTable : class, ITable<TColumns, TIndexes, TRows>, new()
    where TColumns : ITableColumnsDefinition
    where TIndexes : TableIndexes<TColumns>


{
    public TTable Configuration
    {
        get
        {
            return new TTable();
        }
    }
}