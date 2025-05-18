using OnlineSchoolForKids.API.DTOs.Diaries;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Entities;
using OnlineSchoolForKids.Core.Specifications.Diaries;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiariesController : ControllerBase
	{ 
		private readonly IMapper _mapper;
		private readonly IDiaryService _diaryService;

		public DiariesController(IMapper mapper, IDiaryService diaryService)
		{
			_mapper = mapper;
			_diaryService = diaryService;
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> AddDiary([FromBody] DiaryDTO diaryDTO)
		{
			var diary = _mapper.Map<DiaryDTO, Diary>(diaryDTO);
			diary.CreationDate  = DateOnly.FromDateTime(DateTime.UtcNow);
			diary.LastUpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

			var added = await _diaryService.AddAsync(diary);

			if (!added)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(diary);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPut("update")]
		public async Task<ActionResult<DiaryDTO>> UpdateDiary([FromBody] DiaryDTO diaryDTO)
		{
			var diaryFromDb = await _diaryService.GetDiaryByIdAsync(diaryDTO.Id);

			if (diaryFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Diary with Id {diaryDTO.Id} Not Found"));

			diaryFromDb.Title = diaryDTO.Title;
			diaryFromDb.Content = diaryDTO.Content;
			diaryFromDb.LastUpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

			var updated = await _diaryService.UpdateAsync(diaryFromDb);

			if (!updated)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(diaryDTO);
		}

		//[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDiary(string id)
		{
			var diaryFromDb = await _diaryService.GetDiaryByIdAsync(id);

			if (diaryFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Diary with Id {id} Not Found"));

			var deleted = await _diaryService.DeleteAsync(diaryFromDb);

			if (!deleted)
				return BadRequest(new BaseErrorResponse(400));
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] DiaryParams diaryParams)
		{
			var diarys = await _diaryService.GetAllDiariesAsync(diaryParams);
			
			var count = await _diaryService.GetCountAsync(diaryParams);


			var diaryDTOs = _mapper.Map<List<Diary>, List<DiaryDTO>>(diarys.ToList());
			
			return Ok(new PaginationResponse<DiaryDTO>
				(diaryParams.PageSize, diaryParams.PageIndex, count, diaryDTOs));
		}



		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var diary = await _diaryService.GetDiaryByIdAsync(id);

			if (diary is null)
				return NotFound(new BaseErrorResponse(404));
			
			var diaryDTO = _mapper.Map<Diary, DiaryDTO>(diary);

			return Ok(diaryDTO);
		}

	}

}
