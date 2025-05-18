using OnlineSchoolForKids.Core.Specifications.Comments;
using OnlineSchoolForKids.Core.Specifications.Diaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Services
{
	public class CommentService : ICommentService
	{
		
		private readonly IUnitOfWork _unitOfWork;

		public CommentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public async Task<bool> AddAsync(Comment comment)
		{
			await _unitOfWork.Repository<Comment>().AddAsync(comment);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> UpdateAsync(Comment comment)
		{

			_unitOfWork.Repository<Comment>().Update(comment);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> DeleteAsync(Comment comment)
		{
			_unitOfWork.Repository<Comment>().Delete(comment);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<IReadOnlyList<Comment>> GetAllCommentsAsync(CommentParams commentParams)
		{
			var spec = new CommentSpecs(commentParams);

			var comments = await _unitOfWork.Repository<Comment>().GetAllWithSpecAsync(spec);

			return comments;
		}

		public async Task<Comment?> GetCommentByIdAsync(string Id)
		{
			return await _unitOfWork.Repository<Comment>().GetByIdAsync(Id);
		}

		public async Task<int> GetCountAsync(CommentParams commentParams)
		{
			var spec = new CommentSpecs(commentParams);

			var count = (await _unitOfWork.Repository<Comment>().GetAllWithSpecAsync(spec)).Count();

			return count;
		}

	}
}
