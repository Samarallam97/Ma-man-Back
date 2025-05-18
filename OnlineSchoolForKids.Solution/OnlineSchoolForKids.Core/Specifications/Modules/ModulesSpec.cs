using OnlineSchoolForKids.Core.Specifications.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.Modules
{
	public class ModulesSpec : Specification<Module>
	{
		public ModulesSpec(ModuleParams moduleParams)
		: base(
		e => (string.IsNullOrEmpty(moduleParams.Search) || e.Name.ToLower().Contains(moduleParams.Search))
				  || (string.IsNullOrEmpty(moduleParams.Search) || e.NameAr.Contains(moduleParams.Search))
				  || (string.IsNullOrEmpty(moduleParams.CategoryId) || e.CategoryId == moduleParams.CategoryId))
		{
			ApplyPagination(moduleParams.PageSize, moduleParams.PageIndex);
		}
		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
	}
}
