namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITableConfiguration<TTable, TColumns, TIndexes, TRows>
    where TTable : ITable<TColumns, TIndexes, TRows>
    where TColumns : ITableColumnsDefinition
    where TIndexes : TableIndexes<TColumns>
{
    TTable Configuration { get; }
}