
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IGenericRepository<Parent> _parentRepo;
        IGenericRepository<Kid> _kidRepo;
        public ParentController(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _parentRepo = _unitOfWork.Repository<Parent>();
            _kidRepo = _unitOfWork.Repository<Kid>();
            _mapper = mapper;
        }

        // parent add minutes to kid

        [HttpPost("add/{parentId}/{kidId}/{minutes}")]
        public async Task<IActionResult> SetTimeLimit(int parentId, int kidId, int minutes)
        { 
            var parent = await _parentRepo.GetByIdAsync(parentId);
            if (parent == null)
                return NotFound(new BaseErrorResponse(404, $"Parent with Id {parentId} not found."));

            var kid = await _kidRepo.GetByIdAsync(kidId);
            if (kid == null)
              return NotFound(new BaseErrorResponse(404, $"Kid with Id {kidId} not found."));

            if (kid.ParentId != parentId)
                return NotFound(new BaseErrorResponse(404, $"Kid with Id {kidId} does not belong to Parent with Id {parentId}."));

            if (minutes < 0 || minutes > 1440)    
                return NotFound(new BaseErrorResponse(400, $"Minutes must be greater than 0."));

            kid.TimeLimitWithMinutes = minutes;
            _kidRepo.Update(kid);
            var result = await _unitOfWork.CompleteAsync();
            
            if (result > 0)
                return Ok("Time limit set successfully.");
            return BadRequest(new BaseErrorResponse(400, "An error occurred while saving to the database."));
        }

        /*var parent = await _parentRepo.GetByIdAsync(parentDTO.Id);
            if (parent == null)
                return NotFound(new BaseErrorResponse(404, $"Parent with Id {parentDTO.Id} not found."));
            
            var kid = parent.Kids.FirstOrDefault(k => k.Id == kidId);
            if (kid == null)
              return NotFound(new BaseErrorResponse(404, $"Kid with Id {kidId} not found."));
            kid.TimeLimitWithMinutes = minutes;
            _parentRepo.Update(parent);
            var result = await _unitOfWork.CompleteAsync();
            
            if (result > 0)
                return Ok("Time limit set successfully.");
            return BadRequest(new BaseErrorResponse(400, "An error occurred while saving to the database."));
       */
    }
}
