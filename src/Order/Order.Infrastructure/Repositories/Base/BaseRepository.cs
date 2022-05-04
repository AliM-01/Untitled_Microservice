using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities.Base;
using Order.Domain.Repositories.Base;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories.Base;
public class BaseRepository<T> : IRepository<T> where T : Entity
{
    #region ctor

    protected readonly OrderDbContext _dbContext;

    public BaseRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region GetAllAsync

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    #endregion

    #region GetAsync

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (!string.IsNullOrEmpty(includeString))
            query = query.Include(includeString);

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query,
                                       (current, include) => current.Include(include));

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    #endregion

    #region GetByIdAsync

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    #endregion

    #region AddAsync

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    #endregion

    #region UpdateAsync

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    #endregion

    #region DeleteAsync

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await GetByIdAsync(id);

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    #endregion
}
