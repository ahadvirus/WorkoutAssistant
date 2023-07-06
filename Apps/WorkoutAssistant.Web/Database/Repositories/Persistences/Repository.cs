using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Web.Database.Contexts;
using WorkoutAssistant.Web.Database.Repositories.Contracts;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Database.Repositories.Persistences;

public class Repository<TEntity, TPrimary> : IRepository<TEntity, TPrimary>
    where TEntity : class, IEntity<TPrimary> where TPrimary : struct
{
    /// <summary>
    /// Access to all database of application
    /// </summary>
    protected ApplicationContext Context { get; }

    /// <summary>
    /// Access to only table want to be use in repository
    /// </summary>
    protected DbSet<TEntity> Set { get; }

    public Repository(ApplicationContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> result = Set;

        if (predicate != null)
        {
            result = result.Where(predicate: predicate);
        }

        return result;
    }

    public Task<TEntity?> GetOrDefaultAsync(TPrimary primary, CancellationToken cancellationToken)
    {
        return Get(predicate: entity => entity.Id.Equals(primary))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Set.AddAsync(entity: entity, cancellationToken: cancellationToken);

        await Context.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Set.Update(entity: entity);
        
        return await Context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Set.Remove(entity: entity);
        
        return await Context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TPrimary primary, CancellationToken cancellationToken)
    {
        return await ExistAsync(primary: primary, cancellationToken: cancellationToken) && await DeleteAsync(
            entity: await GetOrDefaultAsync(primary: primary, cancellationToken: cancellationToken) ?? throw new Exception(message: string.Empty),
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> ExistAsync(TPrimary primary, CancellationToken cancellationToken)
    {
        return ExistAsync(predicate: entity => entity.Id.Equals(primary), cancellationToken: cancellationToken);
    }

    public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return Get(predicate: predicate).AnyAsync(cancellationToken: cancellationToken);
    }
}