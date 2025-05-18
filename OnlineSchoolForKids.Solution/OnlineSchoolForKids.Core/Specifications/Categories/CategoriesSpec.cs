using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.Categories
{
	public class CategoriesSpec : Specification<Category>
	{
        public CategoriesSpec(CategoryParams categoryParams)
            :base(
				  e => (string.IsNullOrEmpty(categoryParams.Search) || e.Name.ToLower().Contains(categoryParams.Search))
                  || (string.IsNullOrEmpty(categoryParams.Search) || e.NameAR.Contains(categoryParams.Search)))
        {
			ApplyPagination(categoryParams.PageSize, categoryParams.PageIndex);
		}
		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
	}
}
