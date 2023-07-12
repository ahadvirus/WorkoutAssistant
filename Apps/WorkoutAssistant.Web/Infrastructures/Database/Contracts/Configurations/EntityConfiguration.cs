using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Web.Infrastructures.Contracts;
using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Contracts.Configurations;

public abstract class EntityConfiguration<TEntity, TPrimary, TTable, TColumns, TIndexes, TRows> :
    IEntityTypeConfiguration<TEntity>,
    ITableConfiguration<TTable, TColumns, TIndexes, TRows>
    where TTable : class, ITable<TColumns, TIndexes, TRows>, new()
    where TColumns : ITableColumnsDefinition
    where TIndexes : TableIndexes<TColumns>
    where TEntity : class, IEntity<TPrimary>
    where TPrimary : struct
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    public TTable Configuration
    {
        get { return new TTable(); }
    }
}