using OnlineSchoolForKids.API.DTOs;
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdultController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Adult> _AdultRepo;

        public AdultController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AdultRepo = _unitOfWork.Repository<Adult>();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTimeLimit(AdultDTO adultDTO)
        {
            var existingAdult = await _AdultRepo.GetByIdAsync(adultDTO.Id);
            if (existingAdult == null)
                return NotFound(new BaseErrorResponse(404, $"Adult with Id {adultDTO.Id} not found."));
            
            existingAdult.TimeLimitWithMinutes = adultDTO.TimeLimitWithMinutes;

            _AdultRepo.Update(existingAdult);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(adultDTO);

            return BadRequest(new BaseErrorResponse(400));

        }




    }
}
