using Core.Entities.Abstract.Base;
using Core.Entities.Concrete.Base;

namespace Core.DataAccess.EntityFramework.Abstract;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IEntity, new();
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}