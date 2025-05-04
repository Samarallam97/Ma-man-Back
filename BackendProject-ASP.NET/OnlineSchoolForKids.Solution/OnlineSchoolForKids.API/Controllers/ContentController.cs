using Microsoft.EntityFrameworkCore;
using OnlineSchoolForKids.Core.Entities;
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

		if (result is not null)
			return Ok(result);

		return BadRequest(new BaseErrorResponse(400));
	}

	[HttpPost("assign_age_group")]
	public async Task<IActionResult> AssignContentToAnAgeGroup([FromQuery] int contentId , int ageGroupId)
	{
		var spec = new Specification<Content>()
		{
			Criteria = c => c.Id == contentId,
			Includes = new()
			{
				c => c.AgeGroups
			}
		};

		var content = await _contentRepo.GetWithSpecAsync(spec);

		var ageGroup = await _unitOfWork.Repository<AgeGroup>().GetByIdAsync(ageGroupId);
		
		if (content is not null && ageGroup is not null)
		{
			content.AgeGroups.Add(ageGroup);
			var result = await _unitOfWork.CompleteAsync();

			if (result > 0)
				return Ok(new BaseErrorResponse(200,"Assigned successfully"));
			
			return BadRequest(new BaseErrorResponse(400));
		}

		return NotFound(new BaseErrorResponse(404, "Content Or AgeGroup with this Id is not found"));
	}
	[HttpGet("age_group/{id}")]
	public async Task<ActionResult<IReadOnlyList<Content>>> GetContentByAgeGroup(int id)
	{
		var spec = new Specification<Content>()
		{
			Criteria = c => c.AgeGroups.Any(A => A.Id == id),
			Includes = new()
			{
				c => c.AgeGroups ,
				c => c.Category,
				c => c.Format
			}
		};
		
		var result = await _unitOfWork.Repository<Content>().GetAllWithSpecAsync(spec);

		if (result is not null)
			return Ok(result);

		return BadRequest(new BaseErrorResponse(400));
	}

	[HttpGet("hide")]

	public async Task<IActionResult> HideContentForAUser(int contentId , int userId)
	{
		var spec = new Specification<Content>()
		{
			Criteria = c => c.Id == contentId,
			Includes = new()
			{
				c => c.UsersHidden
			}
		};
		var content = await _contentRepo.GetWithSpecAsync(spec);
		var user = await _userManager.FindByIdAsync(userId.ToString());

		if(content is not null && user is not null)
		{
			content.UsersHidden.Add(new UserContentHidden 
			{ 
				ContentId = contentId ,
				UserId = userId
			});

			var result = await _unitOfWork.CompleteAsync();
			
			if (result > 0)
				return Ok("Hidden Successfully");

			return BadRequest(new BaseErrorResponse(400));
		}

		return NotFound(new BaseErrorResponse(404));
	}

	[HttpGet("rate")]

	public async Task<IActionResult> RateContent(int contentId, int userId , int stars)
	{
		var spec = new Specification<Content>()
		{
			Criteria = c => c.Id == contentId,
			Includes = new()
			{
				c => c.UsersRated
			}
		};
		var content = await _contentRepo.GetWithSpecAsync(spec);
		var user = await _userManager.FindByIdAsync(userId.ToString());

		if (content is not null && user is not null)
		{
			content.UsersRated.Add(new()
			{
				 ContentId = contentId ,
				 UserId = userId ,
				 Stars = stars,
				 RatingDate = DateTime.UtcNow
			});

			var result = await _unitOfWork.CompleteAsync();

			if (result > 0)
				return Ok("Rated Successfully");

			return BadRequest(new BaseErrorResponse(400));
		}

		return NotFound(new BaseErrorResponse(404));
	}
}
