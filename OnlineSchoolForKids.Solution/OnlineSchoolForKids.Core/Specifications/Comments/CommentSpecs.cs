

namespace OnlineSchoolForKids.Core.Specifications.Comments;

public class CommentSpecs : Specification<Comment>
{
	public CommentSpecs(CommentParams commentParams)
	  : base(
	  e => (string.IsNullOrEmpty(commentParams.Search) || e.Text.ToLower().Contains(commentParams.Search))
			   || (string.IsNullOrEmpty(commentParams.UserId) || e.UserId == commentParams.UserId)
	           || (string.IsNullOrEmpty(commentParams.ModuleId) || e.ModuleId == commentParams.ModuleId))
	{
		ApplyPagination(commentParams.PageSize, commentParams.PageIndex);
		ApplySorting(commentParams.SortDesending);
	}
	private void ApplyPagination(int? pageSize, int? pageIndex)
	{
		Skip = (pageIndex - 1) * pageSize;
		Take = pageSize;
	}

	private void ApplySorting(bool SortDesending)
	{
		if (SortDesending)
		{
			OrderByDesc = c => c.UpdatedAt;
		}
		else
		{
			OrderBy = c => c.UpdatedAt;
		}
	}

	

}
