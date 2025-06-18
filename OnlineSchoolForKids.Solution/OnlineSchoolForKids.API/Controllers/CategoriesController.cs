using OnlineSchoolForKids.API.DTOs;
using OnlineSchoolForKids.API.DTOs.Categories;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Repositories.Interfaces;
using OnlineSchoolForKids.Core.Specifications.Categories;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
		private readonly IMapper _mapper;
		private readonly ICategoryService _categoryService;

		public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
			_mapper = mapper;
			_categoryService = categoryService;
        }

		//[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryToAddOrUpdate categoryDTO)
        {
			var category = _mapper.Map<CategoryToAddOrUpdate, Category>(categoryDTO);

			var added = await _categoryService.AddAsync(category);

			if (!added)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(category);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPut("update")]
		public async Task<ActionResult<CategoryDTO>> UpdateCategory(string Id , [FromBody] CategoryToAddOrUpdate categoryDTO)
		{
			var categoryFromDb = await _categoryService.GetCategoryByIdAsync(Id);

			if (categoryFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Category with Id {Id} Not Found"));

			categoryFromDb.Name = categoryDTO.Name;
			categoryFromDb.NameAR = categoryDTO.NameAR;
			categoryFromDb.Description = categoryDTO.Description;
			categoryFromDb.DescriptionAR = categoryDTO.DescriptionAR;
			categoryFromDb.Color = categoryDTO.Color;
			categoryFromDb.PicutureUrl = categoryDTO.PicutureUrl;

			var updated = await _categoryService.UpdateAsync(categoryFromDb);

			if (!updated)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(categoryDTO);
		}

		//[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(string id)
		{
			var categoryFromDb = await _categoryService.GetCategoryByIdAsync(id);

			if (categoryFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Category with Id {id} Not Found"));

			var deleted = await _categoryService.DeleteAsync(categoryFromDb);

			if (!deleted)
				return BadRequest(new BaseErrorResponse(400));
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] CategoryParams categoryParams)
		{
			var categories = await _categoryService.GetAllCategoriesAsync(categoryParams);
			var count = await _categoryService.GetCountAsync(categoryParams);

			if (categoryParams.Language == "En")
			{

				var categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTOEn>>(categories.ToList());
				return Ok(new PaginationResponse<CategoryDTOEn>
					(categoryParams.PageSize, categoryParams.PageIndex, count, categoryDTOs));
			}
			else if(categoryParams.Language == "Ar")
			{
				var categoryDTOs = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDTOAr>>(categories);
				return Ok(new PaginationResponse<CategoryDTOAr>
						(categoryParams.PageSize, categoryParams.PageIndex, count, categoryDTOs));
			}
			else
				return Ok(new PaginationResponse<Category>
						(categoryParams.PageSize, categoryParams.PageIndex, count, categories));
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id, string? language = null)
		{
			var category = await _categoryService.GetCategoryByIdAsync(id);

			if (category is null)
				return NotFound(new BaseErrorResponse(404));
			CategoryDTO categoryDTO;

			if (language == "En")
				categoryDTO = _mapper.Map<Category, CategoryDTOEn>(category);
			else if (language == "Ar")
				categoryDTO = _mapper.Map<Category, CategoryDTOAr>(category);
			else
				return Ok(category);

			return Ok(categoryDTO);
		}


	}
}
