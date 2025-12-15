using System.Collections.Concurrent;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Abstract.Base;
using Core.Entities.Concrete.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.DataAccess.EntityFramework.Concrete;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private bool _disposed;
    private IDbContextTransaction? _transaction;

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IEntity, new()
    {
        var type = typeof(TEntity);
        if (_repositories.TryGetValue(type, out var repository)) return (IRepository<TEntity>)repository;
        var repositoryInstance = new Repository<TEntity, TContext>(_context);
        _repositories[type] = repositoryInstance;
        return repositoryInstance;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            if (_transaction != null) await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        _disposed = true;
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_transaction != null) await _transaction.DisposeAsync();
        await _context.DisposeAsync();
    }
}