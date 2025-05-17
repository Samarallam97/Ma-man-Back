using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Diary> _diaryRepo;

        public DiaryController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _diaryRepo = _unitOfWork.Repository<Diary>();
        }

        [HttpPost]
        public async Task<IActionResult> AddDiary(DiaryDTO diaryDTO)
        {
            var diary = new Diary()
            {
                Content = diaryDTO.Content,
                UserId = diaryDTO.UserId,
                User = diaryDTO.User
            };
            await _unitOfWork.Repository<Diary>().AddAsync(diary);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(diaryDTO);
            return BadRequest(new BaseErrorResponse(400,message: "Error happened while adding to db"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiaries()
        {
            var result = await _unitOfWork.Repository<Diary>().GetAllAsync();
            if(result.Count()>0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiary(int DiaryId)
        {
            var diary = await _diaryRepo.GetByIdAsync(DiaryId);

            if (diary is null)
                return NotFound(new BaseErrorResponse(404, $"Diary with Id {DiaryId} not found."));

            _diaryRepo.Delete(diary);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiary(DiaryDTO diaryDTO)
        {
            var existingDiary = await _diaryRepo.GetByIdAsync(diaryDTO.Id);

            if (existingDiary is null)
                return NotFound(new BaseErrorResponse(404, $"Diary with Id {diaryDTO.Id} not found."));

            existingDiary.CreationDate = DateTime.Now;
            existingDiary.Content = diaryDTO.Content;

            _diaryRepo.Update(existingDiary);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);

            return BadRequest(new BaseErrorResponse(400));
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiaryById(int id)
        {
            var result = await _unitOfWork.Repository<Diary>().GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404));
        }

    }
}
