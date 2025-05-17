using OnlineSchoolForKids.Core.Repositories.Interfaces; 

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    IGenericRepository<Category> _categoryRepo;


    public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _categoryRepo = _unitOfWork.Repository<Category>();
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryDTO categoryDTO)
    {
       // var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);

        var category = new Category
        {
            Name = categoryDTO.Name,
            Description = categoryDTO.Description
        };

        await _categoryRepo.AddAsync(category);
        var result = await _unitOfWork.CompleteAsync();

        if (result > 0)
            return Ok("Category Added Successfully");
        return BadRequest(new BaseErrorResponse(400, "An ERROR Happened while save to DB"));


    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category is null)
            return NotFound(new BaseErrorResponse(404, $"Category with Id {id} not found."));

        _categoryRepo.Delete(category);
        var result = await _unitOfWork.CompleteAsync();

        if (result > 0) 
            return Ok("Category Deleted Successfully");
        return BadRequest(new BaseErrorResponse(400, "An ERROR Happened while save to DB"));
    }

    [HttpGet("getall")]
    public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
    {

        var categories = await _categoryRepo.GetAllAsync();

        var categoryDTOs = categories.Select(category => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description  
        }).ToList();

        return Ok(categories);
    }
}
