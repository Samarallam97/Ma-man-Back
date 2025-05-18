using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeGroupsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<AgeGroup> _ageGroupRepo;

        public AgeGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ageGroupRepo = _unitOfWork.Repository<AgeGroup>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgeGroups()
        {
            var result = await _unitOfWork.Repository<AgeGroup>().GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AgeGroupDTO>> AddAgeGroup(AgeGroupDTO ageGroupDTO)
        {
            var ageGroup = new AgeGroup()
            {
                Name = ageGroupDTO.Name,
                NameAr = ageGroupDTO.NameAr
            };
            await _unitOfWork.Repository<AgeGroup>().AddAsync(ageGroup);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(ageGroupDTO);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAgeGroup(string AgeGroupId)
        {
            var ageGroup = await _ageGroupRepo.GetByIdAsync(AgeGroupId);

            if (ageGroup is null)
                return NotFound(new BaseErrorResponse(404, $"AgeGroup with Id {AgeGroupId} not found."));

            _ageGroupRepo.Delete(ageGroup);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(ageGroup);

            return BadRequest(new BaseErrorResponse(400));
        }

    }
}
