using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Format> _formateRepo;

        public FormatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _formateRepo = _unitOfWork.Repository<Format>();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FormatDTO>>> GetAllFormats()
        {
            var result = await _unitOfWork.Repository<Format>().GetAllAsync();

            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPost]
        public async Task<ActionResult<FormatDTO>> AddFormat(FormatDTO formatDTO)
        {
            var format = new Format()
            {
                Name = formatDTO.Name
            };
            await _unitOfWork.Repository<Format>().AddAsync(format);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {      
                var resultDTO = new FormatDTO
                {
                    Name = format.Name
                };
                return Ok(resultDTO);
            }
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete]
        public async Task<ActionResult<FormatDTO>> DeleteFormat(int FormatId)
        {
            var format = await _formateRepo.GetByIdAsync(FormatId);

            if (format is null)
                return NotFound(new BaseErrorResponse(404, $"Content with Id {FormatId} not found."));

            _formateRepo.Delete(format);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                var resultDTO = new FormatDTO
                {
                    Name = format.Name
                };
                return Ok(resultDTO);
            };

            return BadRequest(new BaseErrorResponse(400));
        }
    }
}
