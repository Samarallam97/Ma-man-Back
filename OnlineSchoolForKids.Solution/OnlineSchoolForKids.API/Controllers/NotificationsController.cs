using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolForKids.API.DTOs.Notifications;
using OnlineSchoolForKids.API.Helpers;
using OnlineSchoolForKids.Core.Specifications.Notifications;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase { 
	private readonly IMapper _mapper;
	private readonly INotificationService _notificationService;

	public NotificationsController(IMapper mapper, INotificationService notificationService)
	{
		_mapper = mapper;
		_notificationService = notificationService;
	}

	//[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> AddNotification([FromBody] NotificationDTO notificationDTO)
	{
		var notification = _mapper.Map<NotificationDTO, Notification>(notificationDTO);
		notification.CreatedAt  = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
		notification.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

		var added = await _notificationService.AddAsync(notification);

		if (!added)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(notification);
	}

	//[Authorize(Roles = "Admin")]
	[HttpPut("update")]
	public async Task<ActionResult<NotificationDTO>> UpdateNotification([FromBody] NotificationDTO notificationDTO)
	{
		var notificationFromDb = await _notificationService.GetNotificationByIdAsync(notificationDTO.Id);

		if (notificationFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Notification with Id {notificationDTO.Id} Not Found"));

		notificationFromDb.Message = notificationDTO.Message;
		notificationFromDb.MessageAr = notificationDTO.MessageAr;
		notificationFromDb.Type = notificationDTO.Type;
		notificationFromDb.IsRead = notificationDTO.IsRead;
		notificationFromDb.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

		var updated = await _notificationService.UpdateAsync(notificationFromDb);

		if (!updated)
			return BadRequest(new BaseErrorResponse(400));

		return Ok(notificationDTO);
	}

	//[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteNotification(string id)
	{
		var notificationFromDb = await _notificationService.GetNotificationByIdAsync(id);

		if (notificationFromDb is null)
			return NotFound(new BaseErrorResponse(404, $"Notification with Id {id} Not Found"));

		var deleted = await _notificationService.DeleteAsync(notificationFromDb);

		if (!deleted)
			return BadRequest(new BaseErrorResponse(400));
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] NotificationParams notificationParams)
	{
		var notifications = await _notificationService.GetAllNotificationsAsync(notificationParams);

		var count = await _notificationService.GetCountAsync(notificationParams);


		var notificationDTOs = _mapper.Map<List<Notification>, List<NotificationDTO>>(notifications.ToList());

		return Ok(new PaginationResponse<NotificationDTO>
			(notificationParams.PageSize, notificationParams.PageIndex, count, notificationDTOs));
	}



	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		var notification = await _notificationService.GetNotificationByIdAsync(id);

		if (notification is null)
			return NotFound(new BaseErrorResponse(404));

		var notificationDTO = _mapper.Map<Notification, NotificationDTO>(notification);

		return Ok(notificationDTO);
	}

}