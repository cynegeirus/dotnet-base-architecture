using System.Linq.Expressions;
using Core.Entities.Abstract;

namespace Core.DataAccess;

public interface IBaseRepository<T> where T : class, IEntity, new()
{
    T? Get(Expression<Func<T?, bool>> filter);
    List<T> GetList(Expression<Func<T, bool>>? expression = null);
    List<T> GetList();

    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}