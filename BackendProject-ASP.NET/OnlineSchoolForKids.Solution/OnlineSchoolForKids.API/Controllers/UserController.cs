
namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;

	public UserController(UserManager<User> userManager , IMapper mapper)
    {
		_userManager=userManager;
		_mapper=mapper;
	}

	[HttpPost]
	public async Task<ActionResult> AddUser(RegisterDTO userDTO)
	{
		var user = new User()
		{
			UserName = userDTO.UserName,
			Email = userDTO.Email,
			FirstName = userDTO.FirstName,
			LastName = userDTO.LastName,
			PhoneNumber = userDTO.PhoneNumber
		};

		var result = await _userManager.CreateAsync(user, userDTO.Password);

		if (result.Succeeded)
			return Ok("Added Successfully");

		return BadRequest(result.Errors);
	}

	[HttpPut("{email}")]
	public async Task<ActionResult> UpdateUser(string email, UpdateUserDTO updateUserDTO)
	{
		var user = await _userManager.FindByEmailAsync(email);
		
		if (user == null)
		{
			return NotFound(new { Message = $"User with email {email} not found." });
		}

		_mapper.Map(updateUserDTO, user);

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			var userResponse = _mapper.Map<UserResponseDTO>(user);
			return Ok(new { Message = "User updated successfully.", User = userResponse });
		}

		var errors = result.Errors.Select(e => e.Description).ToList();
		return BadRequest(new { Message = "Failed to update user.", Errors = errors });
	}

	[HttpDelete("{email}")]
	public async Task<ActionResult> DeleteUser(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		if (user == null)
		{
			return NotFound(new { Message = $"User with email {email} not found." });
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
		var userDtos = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
		return Ok(userDtos);
	}

	//[HttpGet]
	//public async Task<ActionResult<int>> GetTotalPoints(int userId)
	//{
	//}

}
