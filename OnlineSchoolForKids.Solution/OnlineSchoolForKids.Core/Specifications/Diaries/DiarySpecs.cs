using OnlineSchoolForKids.Core.Specifications.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.Diaries
{
	public class DiarySpecs : Specification<Diary>
	{
		public DiarySpecs(DiaryParams diaryParams)
		  : base(
		  e => (string.IsNullOrEmpty(diaryParams.Search) || e.Title.ToLower().Contains(diaryParams.Search))
				   || ((!diaryParams.Date.HasValue) || e.LastUpdateDate== diaryParams.Date)
		           || (string.IsNullOrEmpty(diaryParams.UserId) || e.UserId == diaryParams.UserId))
		{
			ApplyPagination(diaryParams.PageSize, diaryParams.PageIndex);
		}
		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
	}
}
