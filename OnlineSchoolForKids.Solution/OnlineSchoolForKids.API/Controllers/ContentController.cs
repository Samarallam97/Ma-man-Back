using Microsoft.EntityFrameworkCore;
using OnlineSchoolForKids.API.DTOs.Contents;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Entities;
using OnlineSchoolForKids.Core.Repositories.Interfaces;
using OnlineSchoolForKids.Core.Specifications.Contents;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IContentService _contentService;

	public ContentController(IMapper mapper, IContentService contentService)
	{
		_mapper = mapper;
		_contentService = contentService;
	}

	//[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> AddContent([FromBody] ContentToAddOrUpdate contentDTO)
	{
		var content = _mapper.Map<ContentToAddOrUpdate, Content>(contentDTO);

		var added = await _contentService.AddAsync(content);

		if (!added)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(content);
	}

	//[Authorize(Roles = "Admin")]
	[HttpPut("update")]
	public async Task<ActionResult<ContentDto>> UpdateContent([FromBody] ContentToAddOrUpdate contentDTO)
	{
		var contentFromDb = await _contentService.GetContentByIdAsync(contentDTO.Id);

		if (contentFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Content with Id {contentDTO.Id} Not Found"));

		contentFromDb.Title = contentDTO.Title;
		contentFromDb.TitleAr = contentDTO.TitleAr;
		contentFromDb.Description = contentDTO.Description;
		contentFromDb.DescriptionAr = contentDTO.DescriptionAr;
		contentFromDb.ContentUrl = contentDTO.ContentUrl;
		contentFromDb.CreatedByAdminId = contentDTO.CreatedByAdminId;
		contentFromDb.AgeGroups = contentDTO.AgeGroups;



		var updated = await _contentService.UpdateAsync(contentFromDb);

		if (!updated)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(contentDTO);
	}

	//[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteContent(string id)
	{
		var contentFromDb = await _contentService.GetContentByIdAsync(id);

		if (contentFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Content with Id {id} Not Found"));

		var deleted = await _contentService.DeleteAsync(contentFromDb);

		if (!deleted)
			return BadRequest(new BaseErrorResponse(400));
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] ContentParams contentParams)
	{
		var contents = await _contentService.GetAllContentsAsync(contentParams);
		var count = await _contentService.GetCountAsync(contentParams);

		if (contentParams.Language == "En")
		{

			var contentDTOs = _mapper.Map<List<Content>, List<ContentDTOEn>>(contents.ToList());
			return Ok(new PaginationResponse<ContentDTOEn>
				(contentParams.PageSize, contentParams.PageIndex, count, contentDTOs));
		}
		else
		{
			var contentDTOs = _mapper.Map<IReadOnlyList<Content>, IReadOnlyList<ContentDTOAr>>(contents);
			return Ok(new PaginationResponse<ContentDTOAr>
					(contentParams.PageSize, contentParams.PageIndex, count, contentDTOs));
		}
	}


	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id, string language)
	{
		var content = await _contentService.GetContentByIdAsync(id);

		if (content is null)
			return NotFound(new BaseErrorResponse(404));
		ContentDto contentDTO;

		if (language == "En")
			contentDTO = _mapper.Map<Content, ContentDTOEn>(content);
		else
			contentDTO = _mapper.Map<Content, ContentDTOAr>(content);

		return Ok(contentDTO);
	}


}
