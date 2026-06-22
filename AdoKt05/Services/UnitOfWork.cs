using AdoKt05.Data;

namespace AdoKt05.Services;

public interface IUnitOfWork : IDisposable
{
	IRepository<ProductEntity, Guid> Products { get; }
	IRepository<CategoryEntity, Guid> Categories { get; }
	Task<int> SaveAsync();
}

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _context;

	public UnitOfWork(AppDbContext context)
	{
		_context = context;
	}

	public IRepository<ProductEntity, Guid> Products => field ??= new Repository<ProductEntity, Guid>(_context);
	public IRepository<CategoryEntity, Guid> Categories => field ??= new Repository<CategoryEntity, Guid>(_context);

	public async Task<int> SaveAsync()
	{
		return await _context.SaveChangesAsync();
	}

	void IDisposable.Dispose()
	{
		_context.Dispose();
		GC.SuppressFinalize(this);
	}
}
