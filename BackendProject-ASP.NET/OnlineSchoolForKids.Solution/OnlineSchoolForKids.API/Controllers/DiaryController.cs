using Microsoft.AspNetCore.Http.HttpResults;
using OnlineSchoolForKids.Core.Entities;
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IGenericRepository<Diary> _diaryRepo;

        public DiaryController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _diaryRepo = _unitOfWork.Repository<Diary>();
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<ActionResult<DiaryDTO>> AddDiary(DiaryDTO diaryDTO)
        {
            var diary = new Diary
            {
                Id = diaryDTO.Id,
                Content = diaryDTO.Content,
                UserId = diaryDTO.UserId,
            };

            await _diaryRepo.AddAsync(diary);
            var result = await _unitOfWork.CompleteAsync();
           
            if (result > 0)
                return Ok("Diary Added Successfully");
            return BadRequest(new BaseErrorResponse(400, "An ERROR Happened while save to DB"));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<DiaryDTO>> UpdateDiary(int id, DiaryDTO diaryDTO)
        {
            var existingDiary = await _diaryRepo.GetByIdAsync(id);

            if (existingDiary == null)
                return NotFound(new BaseErrorResponse(404, $"Diary with Id {diaryDTO.Id} not found."));
            
            existingDiary.Content = diaryDTO.Content;
            existingDiary.UserId = diaryDTO.UserId;

            _diaryRepo.Update(existingDiary);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(diaryDTO);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<DiaryDTO>> DeleteDiary(int id)
        {
            var Diary = await _diaryRepo.GetByIdAsync(id);

            if (Diary is null)
                return NotFound(new BaseErrorResponse(404, $"Content with Id {id} not found."));

            _diaryRepo.Delete(Diary);
            var result = await _unitOfWork.CompleteAsync();

            if(result > 0)
                return Ok("Diary Deleted Successfully");
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IReadOnlyList<Diary>>> GetByUserId(int id)
        {
            var spec = new Specification<Diary>(C => C.UserId == id);
            var Result = await _diaryRepo.GetAllWithSpecAsync(spec);

            if (Result.Count > 0)
                return Ok(Result);
            return NotFound(new BaseErrorResponse(404, $"Diary with UserId {id} not found."));
        }

    }
}
