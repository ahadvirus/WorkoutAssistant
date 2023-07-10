namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITableColumns<T> where T : ITableColumnsDefinition
{
    T ColumnsName { get; }
}