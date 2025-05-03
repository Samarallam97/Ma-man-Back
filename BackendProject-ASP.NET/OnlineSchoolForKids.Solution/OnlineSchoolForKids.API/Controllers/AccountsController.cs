using Role = OnlineSchoolForKids.Core.Entities.Role;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
	private readonly IAuthenticationService _authenticationService;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;

	public AccountsController(IAuthenticationService authenticationService , UserManager<User> userManager , RoleManager<Role> roleManager )
        {
		_authenticationService=authenticationService;
		_userManager=userManager;
		_roleManager=roleManager;
		}


	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDTO model)
	{
		var user = new User() 
		{ 
			UserName = model.UserName ,
			Email = model.Email ,
			FirstName = model.FirstName ,
			LastName = model.LastName ,
			PhoneNumber = model.PhoneNumber 
		};

		var result = await _userManager.CreateAsync(user , model.Password);

		if (result.Succeeded) 
			return Ok("Registered Successfully");

		return BadRequest(result.Errors);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDTO model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName);

		if(user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
		{
			var token = await _authenticationService.CreateTokenAsync(user);

			return Ok(token);
		}

		return Unauthorized();
	}


	[HttpPost("add-role")]
	public async Task<IActionResult> AddRole([FromBody] string role)
	{
		if(!await _roleManager.RoleExistsAsync(role))
		{
			var result = await _roleManager.CreateAsync(new Role(role));

			if (result.Succeeded)
				return Ok(new { message = "Created Successfully" });

			return BadRequest(result.Errors);
		}

		return BadRequest(new { message = "Role already exists" });
	}

	[HttpPost("assign-role")]
	public async Task<IActionResult> AssignRole([FromBody] UserRoleDTO model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName);

		if (user is null)
			return BadRequest("User Not found");

		var result = await _userManager.AddToRoleAsync(user, model.Role);

		if (result.Succeeded)
			return Ok("Role Assigned Successfully");

		return BadRequest(result.Errors);

	}
}

