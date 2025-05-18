using OnlineSchoolForKids.Core.Specifications.Comments;
using OnlineSchoolForKids.Core.Specifications.Diaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface ICommentService
	{
		Task<bool> AddAsync(Comment comment);
		Task<bool> UpdateAsync(Comment comment);
		Task<bool> DeleteAsync(Comment comment);

		Task<Comment?> GetCommentByIdAsync(string Id);
		Task<int> GetCountAsync(CommentParams commentParams);
		Task<IReadOnlyList<Comment>> GetAllCommentsAsync(CommentParams commentParams);
	}
}
