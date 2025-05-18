using OnlineSchoolForKids.Core.Specifications.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Services;

public class CategoryService : ICategoryService
{
	private readonly IUnitOfWork _unitOfWork;

	public CategoryService(IUnitOfWork unitOfWork)
	{
		_unitOfWork=unitOfWork;
	}
	public async Task<bool> AddAsync(Category category)
	{
		await _unitOfWork.Repository<Category>().AddAsync(category);
		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}

	public async Task<bool> UpdateAsync(Category category)
	{

		_unitOfWork.Repository<Category>().Update(category);

		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}
	public async Task<bool> DeleteAsync(Category category)
	{
		_unitOfWork.Repository<Category>().Delete(category);

		var result = await _unitOfWork.CompleteAsync();

		return result > 0 ? true : false;
	}

	public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams)
	{
		var spec = new CategoriesSpec(categoryParams);

		var categories = await _unitOfWork.Repository<Category>().GetAllWithSpecAsync(spec);

		return categories;
	}

	public async Task<Category?> GetCategoryByIdAsync(string Id)
	{
		return  await _unitOfWork.Repository<Category>().GetByIdAsync(Id);
	}

	public async Task<int> GetCountAsync(CategoryParams categoryParams)
	{
		var spec = new CategoriesSpec(categoryParams);

		var count = (await _unitOfWork.Repository<Category>().GetAllWithSpecAsync(spec)).Count();

		return count;
	}


}
