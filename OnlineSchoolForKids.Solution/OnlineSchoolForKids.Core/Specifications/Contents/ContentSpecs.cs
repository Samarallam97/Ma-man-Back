using OnlineSchoolForKids.Core.Specifications.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.Contents
{
	public class ContentSpecs : Specification<Content>
	{
        public ContentSpecs(ContentParams contentParams)
		: base(
		e => (string.IsNullOrEmpty(contentParams.Search) || e.Title.ToLower().Contains(contentParams.Search))
				  || (string.IsNullOrEmpty(contentParams.Search) || e.TitleAr.Contains(contentParams.Search))
				 || (string.IsNullOrEmpty(contentParams.ModuleId) || e.ModuleId == contentParams.ModuleId)
		         || (string.IsNullOrEmpty(contentParams.AgeGroupId) || e.AgeGroups.Select(a => a.Id).Contains(contentParams.AgeGroupId)))
		{
			ApplyPagination(contentParams.PageSize, contentParams.PageIndex);
		}
		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
    }
}
