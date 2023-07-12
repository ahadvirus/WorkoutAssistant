namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

public interface IForeignTable<TTable, TColumns, TIndexes, TRows>
    where TTable : ITable<TColumns, TIndexes, TRows>
    where TColumns : ITableColumnsDefinition
    where TIndexes : TableIndexes<TColumns>
{
    TTable Foreign { get; }
}

public interface IForeignTable<TTableOne, TOneColumns, TOneIndexes, TOneRows, TTableTwo, TTwoColumns, TTwoIndexes, TTwoRows> :
    IForeignTable<TTableOne, TOneColumns, TOneIndexes, TOneRows>
    where TTableOne : ITable<TOneColumns, TOneIndexes, TOneRows>
    where TOneColumns : ITableColumnsDefinition
    where TOneIndexes : TableIndexes<TOneColumns>
    where TTableTwo : ITable<TTwoColumns, TTwoIndexes, TTwoRows>
    where TTwoColumns : ITableColumnsDefinition
    where TTwoIndexes : TableIndexes<TTwoColumns>
{
    TTableTwo ForeignTwo { get; }
}