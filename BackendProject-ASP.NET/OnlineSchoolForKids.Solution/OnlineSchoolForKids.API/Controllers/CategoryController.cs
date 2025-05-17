using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Category> _categoryRepo;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = _unitOfWork.Repository<Category>();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                CreatedByAdmin = categoryDTO.CreatedByAdmin,
                CreatedByAdminId = categoryDTO.CreatedByAdminId,
                Modules = categoryDTO.Modules
            };
            await _unitOfWork.Repository<Category>().AddAsync(category);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(categoryDTO);
            return BadRequest(new BaseErrorResponse(400, message: "Error happened while adding to db"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _unitOfWork.Repository<Category>().GetAllAsync();
            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(CategoryId);

            if (category is null)
                return NotFound(new BaseErrorResponse(404, $"Category with Id {CategoryId} not found."));

            _categoryRepo.Delete(category);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            var existingCategory = await _categoryRepo.GetByIdAsync(categoryDTO.Id);

            if (existingCategory is null)
                return NotFound(new BaseErrorResponse(404, $"Category with Id {categoryDTO.Id} not found."));

            existingCategory.Name = categoryDTO.Name;
            existingCategory.Description = categoryDTO.Description;
            existingCategory.CreatedByAdmin = categoryDTO.CreatedByAdmin;
            existingCategory.CreatedByAdminId = categoryDTO.CreatedByAdminId;
            existingCategory.Modules = categoryDTO.Modules;

            _categoryRepo.Update(existingCategory);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);

            return BadRequest(new BaseErrorResponse(400));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404));
        }
    }
}
