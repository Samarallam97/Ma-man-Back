using OnlineSchoolForKids.API.DTOs.TODOs;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Repositories.Interfaces;
using OnlineSchoolForKids.Core.Specifications.TODOs;

namespace OnlineSchoolForKids.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDosController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ITODOService _todoService;

		public ToDosController(IMapper mapper, ITODOService todoService)
		{
			_mapper = mapper;
			_todoService = todoService;
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> AddTODO([FromBody] ToDoDTO todoDTO)
		{
			var todo = _mapper.Map<ToDoDTO, TODO>(todoDTO);
			todo.CreationDate  = DateOnly.FromDateTime(DateTime.UtcNow);
			todo.LastUpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

			var added = await _todoService.AddAsync(todo);

			if (!added)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(todo);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPut("update")]
		public async Task<ActionResult<ToDoDTO>> UpdateTODO([FromBody] ToDoDTO todoDTO)
		{
			var todoFromDb = await _todoService.GetTODOByIdAsync(todoDTO.Id);

			if (todoFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"TODO with Id {todoDTO.Id} Not Found"));

			todoFromDb.Status = todoDTO.Status;
			todoFromDb.Content = todoDTO.Content;
			todoFromDb.LastUpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

			var updated = await _todoService.UpdateAsync(todoFromDb);

			if (!updated)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(todoDTO);
		}

		//[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTODO(string id)
		{
			var todoFromDb = await _todoService.GetTODOByIdAsync(id);

			if (todoFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"TODO with Id {id} Not Found"));

			var deleted = await _todoService.DeleteAsync(todoFromDb);

			if (!deleted)
				return BadRequest(new BaseErrorResponse(400));
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] TODOParams todoParams)
		{
			var todos = await _todoService.GetAllTODOAsync(todoParams);

			var count = await _todoService.GetCountAsync(todoParams);


			var todoDTOs = _mapper.Map<List<TODO>, List<ToDoDTO>>(todos.ToList());

			return Ok(new PaginationResponse<ToDoDTO>
				(todoParams.PageSize, todoParams.PageIndex, count, todoDTOs));
		}



		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var todo = await _todoService.GetTODOByIdAsync(id);

			if (todo is null)
				return NotFound(new BaseErrorResponse(404));

			var todoDTO = _mapper.Map<TODO, ToDoDTO>(todo);

			return Ok(todoDTO);
		}

	}
}