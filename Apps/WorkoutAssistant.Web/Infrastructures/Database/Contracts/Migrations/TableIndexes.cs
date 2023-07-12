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

public abstract class TableIndexes<TColumns, 
    TForeign, 
    TForeignColumns,
    TForeignIndexes, 
    TForeignRows> : TableIndexes<TColumns>
    where TColumns : ITableColumnsDefinition
    where TForeign : ITable<TForeignColumns, TForeignIndexes, TForeignRows>
    where TForeignColumns : ITableColumnsDefinition
    where TForeignIndexes : TableIndexes<TForeignColumns>
{
    protected TForeign Foreign { get; }

    protected TableIndexes(string tableName, TColumns columnsName, TForeign foreign) : base(tableName: tableName,
        columnsName: columnsName)
    {
        Foreign = foreign;
    }
}

public abstract class TableIndexes<TColumns, 
    TForeignOne, 
    TForeignOneColumns,
    TForeignOneIndexes, 
    TForeignOneRows,
    TForeignTwo, 
    TForeignTwoColumns,
    TForeignTwoIndexes, 
    TForeignTwoRows> : TableIndexes<TColumns,TForeignOne, 
    TForeignOneColumns,
    TForeignOneIndexes, 
    TForeignOneRows>
    where TColumns : ITableColumnsDefinition
    where TForeignOne : ITable<TForeignOneColumns, TForeignOneIndexes, TForeignOneRows>
    where TForeignOneColumns : ITableColumnsDefinition
    where TForeignOneIndexes : TableIndexes<TForeignOneColumns>
    where TForeignTwo : ITable<TForeignTwoColumns, TForeignTwoIndexes, TForeignTwoRows>
    where TForeignTwoColumns : ITableColumnsDefinition
    where TForeignTwoIndexes : TableIndexes<TForeignTwoColumns>
{
    
    protected TForeignTwo ForeignTwo { get; }

    protected TableIndexes(string tableName, TColumns columnsName, TForeignOne foreign, TForeignTwo foreignTwo) : base(tableName: tableName,
        columnsName: columnsName, foreign: foreign)
    {
        ForeignTwo = foreignTwo;
    }
}