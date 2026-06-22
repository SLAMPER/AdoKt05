using AdoKt05.Data;
using Microsoft.EntityFrameworkCore;

namespace AdoKt05.Services;

public interface IRepository<T, in TKey>
	where T : class
	where TKey : notnull
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(TKey id);
	void Create(T entity);
	void Update(T entity);
	Task DeleteAsync(TKey id);
}

public class Repository<T, TKey> : IRepository<T, TKey>
	where T : class
	where TKey : notnull
{
	private readonly AppDbContext _context;
	private readonly DbSet<T> _dbSet;

	public Repository(AppDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(TKey id)
	{
		return await _dbSet.FindAsync(id);
	}

	public void Create(T entity)
	{
		_dbSet.Add(entity);
	}

	public void Update(T entity)
	{
		_dbSet.Update(entity);
	}

	public async Task DeleteAsync(TKey id)
	{
		var entity = await _dbSet.FindAsync(id);
		if (entity != null)
		{
			_dbSet.Remove(entity);
		}
	}
}
