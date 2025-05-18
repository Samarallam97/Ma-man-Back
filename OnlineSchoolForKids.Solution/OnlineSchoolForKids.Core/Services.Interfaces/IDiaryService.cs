using OnlineSchoolForKids.Core.Specifications.Diaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface IDiaryService
	{
		Task<bool> AddAsync(Diary diary);
		Task<bool> UpdateAsync(Diary diary);
		Task<bool> DeleteAsync(Diary diary);

		Task<Diary?> GetDiaryByIdAsync(string Id);
		Task<int> GetCountAsync(DiaryParams diaryParams);
		Task<IReadOnlyList<Diary>> GetAllDiariesAsync(DiaryParams diaryParams);
	}
}
