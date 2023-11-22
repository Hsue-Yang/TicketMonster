using System.Linq.Expressions;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TicketMonster.Infrastructure.Data;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly TicketMonsterContext DbContext;

    public EfRepository(TicketMonsterContext dbContext)
    {
        DbContext = dbContext;
    }


    public T Add(T entity)
    {
        DbContext.Set<T>().Add(entity);
        DbContext.SaveChanges();
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        DbContext.Set<T>().AddRange(entities);
        DbContext.SaveChanges();
        return entities;
    }

    public async Task<T> AddAsync(T entity)
    {
        DbContext.Set<T>().Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        DbContext.Set<T>().AddRange(entities);
        await DbContext.SaveChangesAsync();
        return entities;
    }

    public T Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
        DbContext.SaveChanges();
        return entity;
    }

    public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
    {
        DbContext.Set<T>().UpdateRange(entities);
        DbContext.SaveChanges();
        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        DbContext.Set<T>().UpdateRange(entities);
        await DbContext.SaveChangesAsync();
        return entities;
    }

    public void Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        DbContext.SaveChanges();
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        DbContext.Set<T>().RemoveRange(entities);
        DbContext.SaveChanges();
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        DbContext.Set<T>().RemoveRange(entities);
        await DbContext.SaveChangesAsync();
    }


    // 找單筆
    public T GetById<TId>(TId id)
    {
        return DbContext.Set<T>().Find(new object[] { id });
    }


    // 非同步找單筆
    public async Task<T> GetByIdAsync<TId>(TId id)
    {
        return await DbContext.Set<T>().FindAsync(new object[] { id });
    }

    // 找第一筆回傳
    public T FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return DbContext.Set<T>().FirstOrDefault(expression) ?? throw new Exception("Entity not found");
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(expression) ?? throw new Exception("Entity not found");
    }

    public T SingleOrDefault(Expression<Func<T, bool>> expression)
    {
        return DbContext.Set<T>().SingleOrDefault(expression) ?? throw new Exception("Entity not found");
    }

    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await DbContext.Set<T>().SingleOrDefaultAsync(expression) ?? throw new Exception("Entity not found");
    }

    // 有沒有資料 回傳布林
    public bool Any(Expression<Func<T, bool>> expression)
    {
        return DbContext.Set<T>().Any(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await DbContext.Set<T>().AnyAsync(expression);
    }

    // 整張資料表
    public async Task<List<T>> ListAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
    {
        return await DbContext.Set<T>().Where(expression).ToListAsync();
    }

    // order
    public async Task<List<T>> OrderByAsync<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        return await DbContext.Set<T>().OrderBy(keySelector).ToListAsync();
    }
   
    //Test
    public async Task<List<T>> TakeAsync(int count)
    {
        return await DbContext.Set<T>().Take(count).ToListAsync();
    }

    //Test
    public async Task<List<T>> OrderByDescendingAsync<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        return await DbContext.Set<T>().OrderByDescending(keySelector).ToListAsync();
    }


    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return DbContext.Set<T>().Where(expression);
    }

	public IQueryable<T> GetAllReadOnly()
	{
		return DbContext.Set<T>().AsNoTracking();
	}
	public IQueryable<T> Query()
	{
		return  DbContext.Set<T>(); 
	}

    public async Task<int> CountAsync(Expression<Func<T,bool>> expression)
    {
        return await DbContext.Set<T>().CountAsync(expression);
    }

    // 取分頁
	public async Task<List<T>> ListAsyncPage(Expression<Func<T, bool>> expression, int page, int pageSize)
	{
		return await DbContext.Set<T>().Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
	}
}