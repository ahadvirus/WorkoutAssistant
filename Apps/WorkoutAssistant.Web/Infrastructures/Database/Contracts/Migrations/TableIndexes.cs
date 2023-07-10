namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public abstract class TableIndexes<T> where T : ITableColumnsDefinition
{
    protected T ColumnsName { get; }
    
    protected string TableName { get; }

    protected TableIndexes(string tableName, T columnsName)
    {
        ColumnsName = columnsName;
        TableName = tableName;
    }
}