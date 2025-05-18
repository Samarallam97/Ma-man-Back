using OnlineSchoolForKids.Core.Specifications.Diaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Services
{
	public class DiaryService : IDiaryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DiaryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public async Task<bool> AddAsync(Diary diary)
		{
			await _unitOfWork.Repository<Diary>().AddAsync(diary);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> UpdateAsync(Diary diary)
		{

			_unitOfWork.Repository<Diary>().Update(diary);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> DeleteAsync(Diary diary)
		{
			_unitOfWork.Repository<Diary>().Delete(diary);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<IReadOnlyList<Diary>> GetAllDiariesAsync(DiaryParams diaryParams)
		{
			var spec = new DiarySpecs(diaryParams);

			var diarys = await _unitOfWork.Repository<Diary>().GetAllWithSpecAsync(spec);

			return diarys;
		}

		public async Task<Diary?> GetDiaryByIdAsync(string Id)
		{
			return await _unitOfWork.Repository<Diary>().GetByIdAsync(Id);
		}

		public async Task<int> GetCountAsync(DiaryParams diaryParams)
		{
			var spec = new DiarySpecs(diaryParams);

			var count = (await _unitOfWork.Repository<Diary>().GetAllWithSpecAsync(spec)).Count();

			return count;
		}

	}
}
