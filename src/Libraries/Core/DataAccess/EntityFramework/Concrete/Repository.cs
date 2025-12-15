using System.Linq.Expressions;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Abstract.Base;
using Core.Entities.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework.Concrete;

public class Repository<TEntity, TContext>(TContext context) : IRepository<TEntity>
    where TEntity : BaseEntity, IEntity, new()
    where TContext : DbContext
{
    protected readonly TContext Context = context;
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public TEntity? Get(Expression<Func<TEntity, bool>> filter)
    {
        return DbSet.FirstOrDefault(filter);
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null ? DbSet.AsNoTracking().ToList() : DbSet.AsNoTracking().Where(filter).ToList();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public void HardDelete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public bool Any(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null ? DbSet.Any() : DbSet.Any(filter);
    }

    public int Count(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null ? DbSet.Count() : DbSet.Count(filter);
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.Now;
        Update(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
        }

        UpdateRange(entities);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        return filter == null ? await DbSet.AsNoTracking().ToListAsync(cancellationToken) : await DbSet.AsNoTracking().Where(filter).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        return filter == null ? await DbSet.AnyAsync(cancellationToken) : await DbSet.AnyAsync(filter, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        return filter == null ? await DbSet.CountAsync(cancellationToken) : await DbSet.CountAsync(filter, cancellationToken);
    }
}