using OnlineSchoolForKids.Core.Specifications.Diaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.TODOs
{
	public class TODOSpec : Specification<TODO>
	{
		public TODOSpec(TODOParams todoParams)
		  : base(
		  e => (string.IsNullOrEmpty(todoParams.Search) || e.Content.ToLower().Contains(todoParams.Search))
				   || ((!todoParams.Date.HasValue) || e.LastUpdateDate== todoParams.Date)
				   || (string.IsNullOrEmpty(todoParams.UserId) || e.UserId == todoParams.UserId)
				   ||  e.Status == todoParams.Status)
		{
			ApplyPagination(todoParams.PageSize, todoParams.PageIndex);
		}
		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
	}
}
