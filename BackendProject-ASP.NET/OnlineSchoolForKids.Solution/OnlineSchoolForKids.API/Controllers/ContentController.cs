using Microsoft.EntityFrameworkCore;
using OnlineSchoolForKids.Core.Entities;
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    IGenericRepository<Content> _contentRepo;

    public ContentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _contentRepo = _unitOfWork.Repository<Content>();
    }

    [HttpPost]
    public async Task<IActionResult> AddContent(ContentDTO contentDTO)
    {
        var content = new Content()
        {
            Title = contentDTO.Title,
            Description = contentDTO.Description,
            Type = contentDTO.Type,
            ContentUrl = contentDTO.ContentUrl,
            ModuleId = contentDTO.ModuleId,
            Module = contentDTO.Module,
            CreatedByAdmin = contentDTO.CreatedByAdmin,
            CreatedByAdminId = contentDTO.CreatedByAdminId, 
            AgeGroups = contentDTO.AgeGroups,
        };
        await _unitOfWork.Repository<Content>().AddAsync(content);
        var result = await _unitOfWork.CompleteAsync();

        if (result > 0)
            return Ok(contentDTO);
        return BadRequest(new BaseErrorResponse(400, message: "Error happened while adding to db"));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiaries()
    {
        var result = await _unitOfWork.Repository<Content>().GetAllAsync();
        if (result.Count() > 0)
            return Ok(result);
        return BadRequest(new BaseErrorResponse(400));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteContent(int ContentId)
    {
        var content = await _contentRepo.GetByIdAsync(ContentId);

        if (content is null)
            return NotFound(new BaseErrorResponse(404, $"Content with Id {ContentId} not found."));

        _contentRepo.Delete(content);
        var result = await _unitOfWork.CompleteAsync();

        if (result > 0)
            return Ok(result);
        return BadRequest(new BaseErrorResponse(400));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContent(ContentDTO contentDTO)
    {
        var existingContent = await _contentRepo.GetByIdAsync(contentDTO.Id);

        if (existingContent is null)
            return NotFound(new BaseErrorResponse(404, $"Content with Id {contentDTO.Id} not found."));

        existingContent.Title = contentDTO.Title;
        existingContent.Description = contentDTO.Description;
        existingContent.Type = contentDTO.Type;
        existingContent.ContentUrl = contentDTO.ContentUrl;
        existingContent.ModuleId = contentDTO.ModuleId;
        existingContent.Module = contentDTO.Module;
        existingContent.CreatedByAdmin = contentDTO.CreatedByAdmin;
        existingContent.CreatedByAdminId = contentDTO.CreatedByAdminId;
        existingContent.AgeGroups = contentDTO.AgeGroups;

        _contentRepo.Update(existingContent);

        var result = await _unitOfWork.CompleteAsync();

        if (result > 0)
            return Ok(result);

        return BadRequest(new BaseErrorResponse(400));

    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetContentById(int id)
    {
        var result = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
        if (result != null)
            return Ok(result);
        return NotFound(new BaseErrorResponse(404));
    }


}
