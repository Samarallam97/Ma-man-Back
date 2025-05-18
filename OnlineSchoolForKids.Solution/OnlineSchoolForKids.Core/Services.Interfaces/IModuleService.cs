using OnlineSchoolForKids.Core.Specifications.Categories;
using OnlineSchoolForKids.Core.Specifications.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface IModuleService
	{
		Task<bool> AddAsync(Module module);
		Task<bool> UpdateAsync(Module module);
		Task<bool> DeleteAsync(Module module);

		Task<Module?> GetModuleByIdAsync(string Id);
		Task<int> GetCountAsync(ModuleParams moduleParams);
		Task<IReadOnlyList<Module>> GetAllModulesAsync(ModuleParams moduleParams);
	}
}
