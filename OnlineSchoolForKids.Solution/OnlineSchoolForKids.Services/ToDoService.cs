using OnlineSchoolForKids.Core.Specifications.Diaries;
using OnlineSchoolForKids.Core.Specifications.TODOs;

namespace OnlineSchoolForKids.Services;

public class TODOService : ITODOService
{
	
	private readonly IUnitOfWork _unitOfWork;

	public TODOService(IUnitOfWork unitOfWork)
	{
		_unitOfWork=unitOfWork;
	}
	public async Task<bool> AddAsync(TODO todo)
	{
		await _unitOfWork.Repository<TODO>().AddAsync(todo);
		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}

	public async Task<bool> UpdateAsync(TODO todo)
	{

		_unitOfWork.Repository<TODO>().Update(todo);

		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}
	public async Task<bool> DeleteAsync(TODO todo)
	{
		_unitOfWork.Repository<TODO>().Delete(todo);

		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}

	public async Task<IReadOnlyList<TODO>> GetAllTODOAsync(TODOParams todoParams)
	{
		var spec = new TODOSpec(todoParams);

		var todos = await _unitOfWork.Repository<TODO>().GetAllWithSpecAsync(spec);

		return todos;
	}

	public async Task<TODO?> GetTODOByIdAsync(string Id)
	{
		return await _unitOfWork.Repository<TODO>().GetByIdAsync(Id);
	}

	public async Task<int> GetCountAsync(TODOParams todoParams)
	{
		var spec = new TODOSpec(todoParams);

		var count = (await _unitOfWork.Repository<TODO>().GetAllWithSpecAsync(spec)).Count();

		return count;
	}

}
