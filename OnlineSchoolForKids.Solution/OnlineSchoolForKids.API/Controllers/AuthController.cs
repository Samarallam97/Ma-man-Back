using Microsoft.AspNetCore.Authorization;
using OnlineSchoolForKids.API.DTOs;
using OnlineSchoolForKids.Core.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IAuthService _authService;

	public AuthController(
	UserManager<ApplicationUser> userManager,
	RoleManager<IdentityRole> roleManager,
	IConfiguration configuration,
	IUnitOfWork unitOfWork,
	IAuthService authService)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
		_unitOfWork = unitOfWork;
		_authService=authService;
	}
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto model)
	{
		//if(ModelState.IsValid)
		// 1. Validate role existence
		if (!await _roleManager.RoleExistsAsync(model.Role))
			return NotFound(new BaseErrorResponse(404, "Role Not found"));

		// 2. Special validation for kids
		if (model.Role == "Kid" && model.ParentId is null)
			return BadRequest(new BaseErrorResponse(400, "Can't add a kid without parent id"));

		// 3. Create user
		var user = new ApplicationUser
		{
			FullName = model.FullName,
			UserName = model.Email,
			Email = model.Email,
			EmailConfirmed = true,
		};

		var result = await _userManager.CreateAsync(user, model.Password);
		if (!result.Succeeded)
			return BadRequest(new ValidationErrorResponse { Errors = result.Errors });

		// 4. Assign role
		await _userManager.AddToRoleAsync(user, model.Role);

		// 5. Additional logic if role is Kid
		if (model.Role == "Kid")
		{
			var parentChildLink = new ParentChild
			{
				ParentId = model.ParentId,
				ChildId = user.Id
			};

			await _unitOfWork.Repository<ParentChild>().AddAsync(parentChildLink);

			var count = await _unitOfWork.CompleteAsync();
			if (count <= 0)
				return BadRequest(new BaseErrorResponse(400, "Failed to link kid to parent"));

			return Ok(new { Message = "Kid added successfully" });
		}

		// 6. Generic success   [return object]
		return Ok(new { Message = "User added successfully" });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);  // find by name
		
		if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
			return Unauthorized(new BaseErrorResponse(401, "Invalid credentials"));

		var claims = await GenerateUserClaims(user);
		
		var accessToken = await _authService.CreateTokenAsync(claims);
		
		var refreshToken = await _authService.GenerateRefreshToken(user);

		Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
		{
			HttpOnly = true,
			SameSite = SameSiteMode.Strict,
			Expires = refreshToken.ExpiresAt,
			//Secure = true // uncomment in production
		});

		return Ok(new
		{
			accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
			expiration = accessToken.ValidTo
		});
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken()
	{
		var refreshToken = Request.Cookies["refreshToken"];
		
		if (string.IsNullOrEmpty(refreshToken))
			return Unauthorized();

		var storedToken = await _unitOfWork.Repository<RefreshToken>().GetWithSpecAsync(new Specification<RefreshToken>
		{
			Criteria = r => r.Token == refreshToken && !r.IsRevoked
		});

		if (storedToken == null || storedToken.IsExpired)
			return Unauthorized();

		var user = await _userManager.FindByIdAsync(storedToken.UserId);
		
		if (user == null)
			return Unauthorized();

		var claims = await GenerateUserClaims(user);
		
		var newAccessToken = await _authService.CreateTokenAsync(claims);

		return Ok(new
		{
			accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
			expiration = newAccessToken.ValidTo
		});
	}


	[HttpGet("roles")]
	public async Task<IActionResult> GetAllRoles()
	{
		return Ok(await _roleManager.Roles.ToListAsync());
	}

	private async Task<List<Claim>> GenerateUserClaims(ApplicationUser user)
	{

		var roles = await _userManager.GetRolesAsync(user);
		//var userClaims = await _userManager.GetClaimsAsync(user);


		var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, user.Id),
		new Claim("fullName", user.FullName ?? ""),
		new Claim("email", user.Email ?? ""),
		new Claim("profilePictureUrl", user.ProfilePictureUrl ?? ""),
		new Claim("dailyUsageLimit", user.DailyUsageLimit.ToString()),
		new Claim("dailyUsageToday", user.DailyUsageToday.ToString()),
		new Claim("lastAccessDate", user.LastAccessDate.ToString("o")), // ISO format
		new Claim(ClaimTypes.Role, roles[0]) ,
		new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())

	};

		return claims;
	}

}

