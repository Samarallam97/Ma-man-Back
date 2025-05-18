using OnlineSchoolForKids.Core.Specifications.Modules;
namespace OnlineSchoolForKids.Services
{
	public class ModuleService : IModuleService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ModuleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public async Task<bool> AddAsync(Module module)
		{
			await _unitOfWork.Repository<Module>().AddAsync(module);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> UpdateAsync(Module module)
		{

			_unitOfWork.Repository<Module>().Update(module);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> DeleteAsync(Module module)
		{
			_unitOfWork.Repository<Module>().Delete(module);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<IReadOnlyList<Module>> GetAllModulesAsync(ModuleParams moduleParams)
		{
			var spec = new ModulesSpec(moduleParams);

			var modules = await _unitOfWork.Repository<Module>().GetAllWithSpecAsync(spec);

			return modules;
		}

		public async Task<Module?> GetModuleByIdAsync(string Id)
		{
			return await _unitOfWork.Repository<Module>().GetByIdAsync(Id);
		}

		public async Task<int> GetCountAsync(ModuleParams moduleParams)
		{
			var spec = new ModulesSpec(moduleParams);

			var count = (await _unitOfWork.Repository<Module>().GetAllWithSpecAsync(spec)).Count();

			return count;
		}

	}
}
