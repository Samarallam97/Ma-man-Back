using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllFormats()
        {
            var result = await _unitOfWork.Repository<Format>().GetAllAsync();

            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPost]
        public async Task<IActionResult> AddFormat(FormatDTO formatDTO)
        {
            var format = new Format()
            {
                Name = formatDTO.Name
            };
            await _unitOfWork.Repository<Format>().AddAsync(format);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormat(int FormatId)
        {
            var format = await _formateRepo.GetByIdAsync(FormatId);

            if (format is null)
                return NotFound(new BaseErrorResponse(404, $"Format with Id {FormatId} not found."));

            _formateRepo.Delete(format);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }
    }
}
