
using OnlineSchoolForKids.API.DTOs.Comments;
using OnlineSchoolForKids.API.DTOs.Diaries;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Repositories.Interfaces;
using OnlineSchoolForKids.Core.Specifications.Comments;
using OnlineSchoolForKids.Core.Specifications.Diaries;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase { 

	private readonly IMapper _mapper;
	private readonly ICommentService _commentService;

	public CommentController(IMapper mapper, ICommentService commentService)
	{
		_mapper = mapper;
		_commentService = commentService;
	}

	//[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDTO)
	{
		var comment = _mapper.Map<CommentDTO, Comment>(commentDTO);
		comment.CreatedAt  = DateOnly.FromDateTime(DateTime.UtcNow);
		comment.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

		var added = await _commentService.AddAsync(comment);

		if (!added)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(comment);
	}

	//[Authorize(Roles = "Admin")]
	[HttpPut("update")]
	public async Task<ActionResult<CommentDTO>> UpdateComment([FromBody] CommentDTO commentDTO)
	{
		var commentFromDb = await _commentService.GetCommentByIdAsync(commentDTO.Id);

		if (commentFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Comment with Id {commentDTO.Id} Not Found"));

		commentFromDb.Text = commentDTO.Text;
		commentFromDb.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

		var updated = await _commentService.UpdateAsync(commentFromDb);

		if (!updated)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(commentDTO);
	}

	//[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteComment(string id)
	{
		var commentFromDb = await _commentService.GetCommentByIdAsync(id);

		if (commentFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Comment with Id {id} Not Found"));

		var deleted = await _commentService.DeleteAsync(commentFromDb);

		if (!deleted)
			return BadRequest(new BaseErrorResponse(400));
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] CommentParams commentParams)
	{
		var comments = await _commentService.GetAllCommentsAsync(commentParams);

		var count = await _commentService.GetCountAsync(commentParams);


		var commentDTOs = _mapper.Map<List<Comment>, List<CommentDTO>>(comments.ToList());

		return Ok(new PaginationResponse<CommentDTO>
			(commentParams.PageSize, commentParams.PageIndex, count, commentDTOs));
	}



	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		var comment = await _commentService.GetCommentByIdAsync(id);

		if (comment is null)
			return NotFound(new BaseErrorResponse(404));

		var commentDTO = _mapper.Map<Comment, CommentDTO>(comment);

		return Ok(commentDTO);
	}

}
