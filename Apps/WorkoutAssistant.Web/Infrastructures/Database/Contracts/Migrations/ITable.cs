namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITable<TColumns, TIndexes, TRows> : ITableName,
    ITableColumns<TColumns>,
    ITableIndexes<TIndexes, TColumns>,
    ITableRows<TRows>
    where TIndexes : TableIndexes<TColumns>
    where TColumns : ITableColumnsDefinition
{
}