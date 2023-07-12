namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface ITable<TColumns, TIndexes, TRows> : ITableName,
    ITableColumns<TColumns>,
    ITableIndexes<TIndexes, TColumns>,
    ITableRows<TRows>
    where TIndexes : TableIndexes<TColumns>
    where TColumns : ITableColumnsDefinition
{
}

public interface ITable<TColumns, TIndexes, TRows, TForeign, TForeignColumns, TForeignIndexes, TForeignRows> :
    ITable<TColumns, TIndexes, TRows>,
    IForeignTable<TForeign, TForeignColumns, TForeignIndexes, TForeignRows>
    where TIndexes : TableIndexes<TColumns>
    where TColumns : ITableColumnsDefinition
    where TForeign : ITable<TForeignColumns, TForeignIndexes, TForeignRows>
    where TForeignColumns : ITableColumnsDefinition
    where TForeignIndexes : TableIndexes<TForeignColumns>
{
}

public interface ITable<TColumns,
    TIndexes,
    TRows,
    TForeignOne,
    TForeignOneColumns,
    TForeignOneIndexes,
    TForeignOneRows,
    TForeignTwo,
    TForeignTwoColumns,
    TForeignTwoIndexes,
    TForeignTwoRows> : ITable<TColumns, TIndexes, TRows, TForeignOne, TForeignOneColumns, TForeignOneIndexes,
        TForeignOneRows>,
    IForeignTable<TForeignOne, TForeignOneColumns, TForeignOneIndexes, TForeignOneRows,
        TForeignTwo, TForeignTwoColumns, TForeignTwoIndexes, TForeignTwoRows>
    where TIndexes : TableIndexes<TColumns>
    where TColumns : ITableColumnsDefinition
    where TForeignOne : ITable<TForeignOneColumns, TForeignOneIndexes, TForeignOneRows>
    where TForeignOneColumns : ITableColumnsDefinition
    where TForeignOneIndexes : TableIndexes<TForeignOneColumns>
    where TForeignTwo : ITable<TForeignTwoColumns, TForeignTwoIndexes, TForeignTwoRows>
    where TForeignTwoColumns : ITableColumnsDefinition
    where TForeignTwoIndexes : TableIndexes<TForeignTwoColumns>
{
}