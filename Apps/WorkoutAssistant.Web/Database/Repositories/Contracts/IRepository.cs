using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Database.Repositories.Contracts;

public interface IRepository<TEntity, TPrimary> where TEntity : IEntity<TPrimary> where TPrimary : struct
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null);
    
    Task<TEntity?> GetOrDefaultAsync(TPrimary primary, CancellationToken cancellationToken);
    
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(TPrimary primary, CancellationToken cancellationToken);
    
    Task<bool> ExistAsync(TPrimary primary, CancellationToken cancellationToken);
    
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}