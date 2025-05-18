using OnlineSchoolForKids.Core.Specifications.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Services
{
	public class ContentService : IContentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ContentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public async Task<bool> AddAsync(Content content)
		{
			await _unitOfWork.Repository<Content>().AddAsync(content);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> UpdateAsync(Content content)
		{

			_unitOfWork.Repository<Content>().Update(content);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> DeleteAsync(Content content)
		{
			_unitOfWork.Repository<Content>().Delete(content);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<IReadOnlyList<Content>> GetAllContentsAsync(ContentParams contentParams)
		{
			var spec = new ContentSpecs(contentParams);

			var contents = await _unitOfWork.Repository<Content>().GetAllWithSpecAsync(spec);

			return contents;
		}

		public async Task<Content?> GetContentByIdAsync(string Id)
		{
			return await _unitOfWork.Repository<Content>().GetByIdAsync(Id);
		}

		public async Task<int> GetCountAsync(ContentParams contentParams)
		{
			var spec = new ContentSpecs(contentParams);

			var count = (await _unitOfWork.Repository<Content>().GetAllWithSpecAsync(spec)).Count();

			return count;
		}

	}
}
