namespace OnlineSchoolForKids.Core.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task AddAsync(T entity);
	 void Update(T entity);
	void Delete(T entity);

	Task<T?> GetByIdAsync(int id);
	Task<IReadOnlyList<T>> GetAllAsync();

	Task<T?> GetWithSpecAsync(ISpecification<T> spec);
	Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
}
