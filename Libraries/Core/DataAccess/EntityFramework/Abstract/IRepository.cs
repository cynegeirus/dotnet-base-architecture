using System.Linq.Expressions;
using Core.Entities.Abstract.Base;

namespace Core.DataAccess.EntityFramework.Abstract;

public interface IRepository<T> where T : class, IEntity, new()
{
    // Sync methods
    T? Get(Expression<Func<T, bool>> filter);
    List<T> GetList(Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void HardDelete(T entity);
    bool Any(Expression<Func<T, bool>>? filter = null);
    int Count(Expression<Func<T, bool>>? filter = null);

    // Async methods
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
}