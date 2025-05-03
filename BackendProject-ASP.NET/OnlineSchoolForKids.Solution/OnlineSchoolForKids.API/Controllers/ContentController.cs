using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	IGenericRepository<Content> _contentRepo;

	public ContentController(IUnitOfWork unitOfWork , UserManager<User> userManager , IMapper mapper)
    {
		_unitOfWork=unitOfWork;
		_contentRepo = _unitOfWork.Repository<Content>();
		_userManager=userManager;
		_mapper=mapper;
	}

    [HttpPost("add")]
	public async Task<ActionResult<ContentDTO>> AddContent(ContentDTO contentDTO)
	{
		var content = _mapper.Map<ContentDTO, Content>(contentDTO);

		await _contentRepo.AddAsync(content);

		var result = await _unitOfWork.CompleteAsync();

		if (result > 0)
			return Ok("Content Added Successfully");

		return BadRequest(new BaseErrorResponse(400 , "An ERROR Happened while save to DB"));
	}


	[HttpPut("update")]
	public async Task<ActionResult<ContentDTO>> UpdateContent(ContentDTO contentDTO)
	{
		var existingContent = await _contentRepo.GetByIdAsync(contentDTO.Id);

		if (existingContent == null)
			return NotFound( new BaseErrorResponse(404 , $"Content with Id {contentDTO.Id} not found."));

		existingContent.Title = contentDTO.Title;
		existingContent.Description = contentDTO.Description;
		existingContent.URL = contentDTO.URL;
		existingContent.CategoryId = contentDTO.CategoryId;
		existingContent.AverageRate = contentDTO.AverageRate;

		_contentRepo.Update(existingContent);

		var result = await _unitOfWork.CompleteAsync();

		if (result > 0)
			return Ok(contentDTO);

		return BadRequest(new BaseErrorResponse(400));
	}

	[HttpDelete("delete/{id}")]
	public async Task<ActionResult<ContentDTO>> DeleteContent(int id)
	{
		var content = await _contentRepo.GetByIdAsync(id);

		if (content is null)
			return NotFound(new BaseErrorResponse(404, $"Content with Id {id} not found."));

		_contentRepo.Delete(content);

		var result = await _unitOfWork.CompleteAsync();

		if (result > 0)
			return Ok(content);

		return BadRequest(new BaseErrorResponse(400));
	}
	
	[HttpGet("category/{id}")]
	public async Task<ActionResult<IReadOnlyList<Content>>> GetContentByCategoryId(int id)
	{
		var spec = new Specification<Content>(C => C.CategoryId == id);

		var result = await _contentRepo.GetAllWithSpecAsync(spec);

		if (result.Count() > 0)
			return Ok(result);

		return BadRequest(new BaseErrorResponse(400));
	}

	[HttpGet("format/{id}")]
	public async Task<ActionResult<IReadOnlyList<Content>>> GetContentByFormatId(int id)
	{
		var spec = new Specification<Content>(C => C.FormatId == id);

		var result = await _contentRepo.GetAllWithSpecAsync(spec);

		if (result.Count() > 0)
			return Ok(result);

		return BadRequest(new BaseErrorResponse(400));
	}

}
