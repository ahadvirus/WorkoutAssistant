namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITableRows<T>
{
    T Rows { get; }
}