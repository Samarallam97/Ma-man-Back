using OnlineSchoolForKids.API.DTOs.Categories;
using OnlineSchoolForKids.API.DTOs.Modules;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Repositories.Interfaces;
using OnlineSchoolForKids.Core.Specifications.Categories;
using OnlineSchoolForKids.Core.Specifications.Modules;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IModuleService _moduleService;
		private readonly UserManager<ApplicationUser> _userManager;

		public ModulesController(IMapper mapper, IModuleService moduleService , UserManager<ApplicationUser> userManager)
		{
			_mapper = mapper;
			_moduleService = moduleService;
			_userManager=userManager;
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> AddModule([FromBody] ModuleToAddOrUpdate moduleDTO)
		{
			var module = _mapper.Map<ModuleToAddOrUpdate, Module>(moduleDTO);

			var added = await _moduleService.AddAsync(module);

			if (!added)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(module);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPut("update")]
		public async Task<ActionResult<ModuleDTO>> UpdateModule(string Id , [FromBody] ModuleToAddOrUpdate moduleDTO)
		{
			var moduleFromDb = await _moduleService.GetModuleByIdAsync(Id);

			if (moduleFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Module with Id {Id} Not Found"));

			moduleFromDb.Name = moduleDTO.Name;
			moduleFromDb.NameAr = moduleDTO.NameAr;
			moduleFromDb.Description = moduleDTO.Description;
			moduleFromDb.DescriptionAr = moduleDTO.DescriptionAr;
			moduleFromDb.Color = moduleDTO.Color;
			moduleFromDb.PicutureUrl = moduleDTO.PicutureUrl;
			moduleFromDb.AverageRating = moduleDTO.AverageRating;

			var updated = await _moduleService.UpdateAsync(moduleFromDb);

			if (!updated)
				return BadRequest(new BaseErrorResponse(400));

			return Ok(moduleDTO);
		}

		//[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteModule(string id)
		{
			var moduleFromDb = await _moduleService.GetModuleByIdAsync(id);

			if (moduleFromDb is null)
				return NotFound(new BaseErrorResponse(404, $"Module with Id {id} Not Found"));

			var deleted = await _moduleService.DeleteAsync(moduleFromDb);

			if (!deleted)
				return BadRequest(new BaseErrorResponse(400));
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] ModuleParams moduleParams)
		{
			var modules = await _moduleService.GetAllModulesAsync(moduleParams);
			var count = await _moduleService.GetCountAsync(moduleParams);

			if (moduleParams.Language == "En")
			{

				var moduleDTOs = _mapper.Map<List<Module>, List<ModuleDTOEn>>(modules.ToList());
				return Ok(new PaginationResponse<ModuleDTOEn>
					(moduleParams.PageSize, moduleParams.PageIndex, count, moduleDTOs));
			}
			else
			{
				var moduleDTOs = _mapper.Map<IReadOnlyList<Module>, IReadOnlyList<ModuleDTOAr>>(modules);
				return Ok(new PaginationResponse<ModuleDTOAr>
						(moduleParams.PageSize, moduleParams.PageIndex, count, moduleDTOs));
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id, string language)
		{
			var module = await _moduleService.GetModuleByIdAsync(id);

			if (module is null)
				return NotFound(new BaseErrorResponse(404));
			ModuleDTO moduleDTO;

			if (language == "En")
				moduleDTO = _mapper.Map<Module, ModuleDTOEn>(module);
			else
				moduleDTO = _mapper.Map<Module, ModuleDTOAr>(module);

			return Ok(moduleDTO);
		}

		[HttpPost("hide")]
		public async Task<IActionResult> HideModule(string moduleId, string userId)
		{
			var user = await _userManager.FindByIdAsync(moduleId);
			if (user is null)
				return NotFound(new BaseErrorResponse(404, $"User with id {userId} not found"));

			var module = await _moduleService.GetModuleByIdAsync(moduleId);
			if (module is null)
				return NotFound(new BaseErrorResponse(404, $"Module with id {moduleId} not found"));

			user.HiddenModules.Add(new HiddenModule()
			{
				Id = Guid.NewGuid().ToString(),
				ApplicationUserId = userId,
				ModuleId= moduleId,
			});

			var result = await _userManager.UpdateAsync(user);

			if(result.Succeeded)
				return Ok();
			else
				return BadRequest();

		}

		[HttpPost("rate")]
		public async Task<IActionResult> RateModule(string moduleId, string userId , int stars)
		{
			var user = await _userManager.FindByIdAsync(moduleId);
			if (user is null)
				return NotFound(new BaseErrorResponse(404, $"User with id {userId} not found"));

			var module = await _moduleService.GetModuleByIdAsync(moduleId);
			if (module is null)
				return NotFound(new BaseErrorResponse(404, $"Module with id {moduleId} not found"));

			module.Ratings.Add(new Rating()
			{
				Id = Guid.NewGuid().ToString(),
				Stars = stars,
				UserId = userId,
				ModuleId = moduleId,
				CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
			});

			var updated = await CalculateAverageRating(module);

			if (updated)
				return Ok();
			else
				return BadRequest();
		}

		private async Task<bool> CalculateAverageRating(Module module)
		{
			var moduleRatings = module.Ratings.Select(m => m.Stars).ToList();
			var averageRating = moduleRatings.Average();
			module.AverageRating = averageRating;

			return await _moduleService.UpdateAsync(module);
		}
	}
}
