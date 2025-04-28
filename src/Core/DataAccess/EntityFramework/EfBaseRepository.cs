using System.Linq.Expressions;
using Core.Entities.Abstract.Base;
using Core.Entities.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;

public class EfBaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : BaseEntity, IEntity, new() where TContext : DbContext, new()
{
    public TEntity? Get(Expression<Func<TEntity?, bool>> filter)
    {
        using var context = new TContext();
        return context.Set<TEntity>().Where(filter).SingleOrDefault(x => x!.IsDeleted == false);
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>>? expression)
    {
        using TContext context = new();
        return expression != null ? context.Set<TEntity>().AsNoTracking().Where(expression).Where(x => x.IsDeleted == false).ToList() : context.Set<TEntity>().AsNoTracking().Where(x => x.IsDeleted == false).ToList();
    }

    public List<TEntity> GetList()
    {
        using TContext context = new();
        return context.Set<TEntity>().AsNoTracking().Where(x => x.IsDeleted == false).ToList();
    }

    public bool Add(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        entity.DeletedDate = DateTime.Now;
        entity.IsUpdated = false;
        entity.IsDeleted = false;

        using TContext context = new();
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;

        return context.SaveChanges() > 0;
    }

    public bool Update(TEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        entity.IsUpdated = true;

        using TContext context = new();
        var updatedEntity = context.Entry(entity);
        updatedEntity.State = EntityState.Modified;

        return context.SaveChanges() > 0;
    }

    public bool Delete(TEntity entity)
    {
        entity.DeletedDate = DateTime.Now;
        entity.IsDeleted = true;

        return Update(entity);
    }
}