using OnlineSchoolForKids.API.DTOs.Auth;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IMapper _mapper;

	public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
	{
		_userManager=userManager;
		_mapper=mapper;
	}

	[HttpPut]
	public async Task<ActionResult> UpdateUser( UserToUpdateDto updateUserDTO)
	{
		var user = await _userManager.FindByIdAsync(updateUserDTO.Id);

		if (user == null)
		{
			return NotFound(new { Message = $"User with id {updateUserDTO.Id} not found." });
		}

		_mapper.Map(updateUserDTO, user);

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			var userResponse = _mapper.Map<UserResponseDTO>(user);
			var roles = await _userManager.GetRolesAsync(user);
			userResponse.Role = roles[0];
			return Ok(new { Message = "User updated successfully.", User = userResponse });
		}

		var errors = result.Errors.Select(e => e.Description).ToList();
		return BadRequest(new { Message = "Failed to update user.", Errors = errors });
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteUser(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
		{
			return NotFound(new { Message = $"User with id {id} not found." });
		}

		var result = await _userManager.DeleteAsync(user);

		if (result.Succeeded)
		{
			return Ok(new { Message = "User deleted successfully." });
		}

		var errors = result.Errors.Select(e => e.Description).ToList();
		return BadRequest(new { Message = "Failed to delete user.", Errors = errors });
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
	{
		var users = _userManager.Users.ToList();

		var userDtos = new List<UserResponseDTO>();

		foreach (var user in users)
        {
			var roles = await _userManager.GetRolesAsync(user);
			var userResponseDTO = new UserResponseDTO
			{
				Id = user.Id,
				FullName = user.FullName,
				ProfilePictureUrl = user.ProfilePictureUrl,
				DailyUsageLimit = user.DailyUsageLimit,
				DailyUsageToday = user.DailyUsageToday,
				LastAccessDate = user.LastAccessDate,
				Role = roles[0]
			};

			userDtos.Add(userResponseDTO);	

		}
		return Ok(userDtos);
	}

}
