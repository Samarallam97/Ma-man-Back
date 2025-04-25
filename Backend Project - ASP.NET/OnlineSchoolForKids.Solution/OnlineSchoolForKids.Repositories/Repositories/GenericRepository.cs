namespace OnlineSchoolForKids.Repositories.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly ApplicationDbContext _context;

	public GenericRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task Add(T entity)
	{
		 await _context.Set<T>().AddAsync(entity);
	}
	public void Update(T entity)
	{
		_context.Set<T>().Update(entity);
	}
	public void Delete(T entity)
	{
		_context.Remove(entity);
	}

	public async Task<IReadOnlyList<T>> GetAllAsync()
	{
		return await _context.Set<T>().ToListAsync();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _context.Set<T>().FindAsync(id);
	}

	public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
	{
		return await GetQuery(spec).ToListAsync();
	}

	public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
	{
		return await GetQuery(spec).FirstOrDefaultAsync();
	}

	/// //////////////////////////////////////////////////////////////////////////
	
	private IQueryable<T> GetQuery(ISpecification<T> spec)
	{
		IQueryable<T> query = _context.Set<T>();

		if (spec.Criteria is not null)
			query = query.Where(spec.Criteria);


		if (spec.OrderBy is not null)
			query = query.OrderBy(spec.OrderBy);
		else if (spec.OrderByDesc is not null)
			query = query.OrderByDescending(spec.OrderByDesc);

		if (spec.Skip is not null)
			query = query.Skip(spec.Skip.Value);
		if (spec.Take is not null)
			query = query.Take(spec.Take.Value);

		query = spec.Includes.Aggregate(query, (oldQuery, includeStat) => oldQuery.Include(includeStat));

		return query;
	}
}
