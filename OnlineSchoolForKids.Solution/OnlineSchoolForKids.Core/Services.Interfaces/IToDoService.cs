using OnlineSchoolForKids.Core.Specifications.Diaries;
using OnlineSchoolForKids.Core.Specifications.TODOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface ITODOService
	{
		Task<bool> AddAsync(TODO todo);
		Task<bool> UpdateAsync(TODO todo);
		Task<bool> DeleteAsync(TODO todo);

		Task<TODO?> GetTODOByIdAsync(string Id);
		Task<int> GetCountAsync(TODOParams todoParams);
		Task<IReadOnlyList<TODO>> GetAllTODOAsync(TODOParams todoParams);
	}
}
