namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITableIndexes<TIndexes, TColumns> where TIndexes : TableIndexes<TColumns> where TColumns : ITableColumnsDefinition
{
    TIndexes IndexesName { get; }
}