namespace OnlineSchoolForKids.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private Hashtable _repositories;


	public UnitOfWork(ApplicationDbContext context)
	{
		_context=context;
		_repositories = new Hashtable();
	}

	public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
	{
		var key = typeof(TEntity).Name;

		if (!_repositories.ContainsKey(key))
		{
			var repository = new GenericRepository<TEntity>(_context);

			_repositories.Add(key, repository);
		}

		return _repositories[key]  as IGenericRepository<TEntity>;
	}

	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public async ValueTask DisposeAsync()
	{
		await _context.DisposeAsync();
	}
}
