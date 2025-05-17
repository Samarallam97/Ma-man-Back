using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Module> _moduleRepo;

        public ModuleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _moduleRepo = _unitOfWork.Repository<Module>();
        }

        [HttpPost]
        public async Task<IActionResult> AddModule(ModuleDTO moduleDTO)
        {
            var module = new Module()
            {
                Name = moduleDTO.Name,
                Description = moduleDTO.Description,
                CategoryId = moduleDTO.CategoryId,
                Category = moduleDTO.Category,
                CreatedByAdmin = moduleDTO.CreatedByAdmin,
                CreatedByAdminId = moduleDTO.CreatedByAdminId,
                AverageRating = moduleDTO.AverageRating,
                Contents = moduleDTO.Contents,
            };
            await _unitOfWork.Repository<Module>().AddAsync(module);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(moduleDTO);
            return BadRequest(new BaseErrorResponse(400,message: "Error happened while adding to db"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var result = await _unitOfWork.Repository<Module>().GetAllAsync();
            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModule(int ModuleId)
        {
            var module = await _moduleRepo.GetByIdAsync(ModuleId);

            if (module is null)
                return NotFound(new BaseErrorResponse(404, $"Module with Id {ModuleId} not found."));

            _moduleRepo.Delete(module);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModule(ModuleDTO moduleDTO)
        {
            var existingModule = await _moduleRepo.GetByIdAsync(moduleDTO.Id);

            if (existingModule is null)
                return NotFound(new BaseErrorResponse(404, $"Module with Id {moduleDTO.Id} not found."));

            existingModule.Name = moduleDTO.Name;
            existingModule.Description = moduleDTO.Description;
            existingModule.Category = moduleDTO.Category;
            existingModule.CategoryId = moduleDTO.CategoryId;
            existingModule.CreatedByAdminId = moduleDTO.CreatedByAdminId;
            existingModule.CreatedByAdmin = moduleDTO.CreatedByAdmin;
            existingModule.AverageRating= moduleDTO.AverageRating;
            existingModule.Comments = moduleDTO.Comments;
            existingModule.Ratings = moduleDTO.Ratings;
            existingModule.Contents = moduleDTO.Contents;

            _moduleRepo.Update(existingModule);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);

            return BadRequest(new BaseErrorResponse(400));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleById(int id)
        {
            var result = await _unitOfWork.Repository<Module>().GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404));
        }



    }
}
