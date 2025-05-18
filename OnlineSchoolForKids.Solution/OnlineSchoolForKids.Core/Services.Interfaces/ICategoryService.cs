using OnlineSchoolForKids.Core.Specifications.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces;

public interface ICategoryService
{
	Task<bool> AddAsync(Category category);
	Task<bool> UpdateAsync(Category category);
	Task<bool> DeleteAsync(Category category);

	Task<Category?> GetCategoryByIdAsync(string Id);
	Task<int> GetCountAsync(CategoryParams categoryParams);
	Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams);
}
