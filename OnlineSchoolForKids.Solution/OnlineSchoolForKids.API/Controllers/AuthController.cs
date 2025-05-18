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
		if (!await _roleManager.RoleExistsAsync(model.Role))
			return NotFound(new BaseErrorResponse(404, "Role Not found"));

		if (model.Role == "Kid")
			if (model.ParentId is null)
				return BadRequest(new BaseErrorResponse(400, "Can't add a kid without parent id"));

		var user = new ApplicationUser
		{
			FullName = model.FullName,
			UserName =model.Email,
			Email = model.Email,
			EmailConfirmed = true,
		};

		var result = await _userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
			return BadRequest(new ValidationErrorResponse() { Errors = result.Errors});

		await _userManager.AddToRoleAsync(user, model.Role);

		if (model.Role == "Kid")
		{
			await _unitOfWork.Repository<ParentChild>().AddAsync(new ParentChild()
			{
				ParentId = model.ParentId,
				ChildId = user.Id
			});
			var count = await _unitOfWork.CompleteAsync();
			if (count > 0)
				return Ok(new { Message = "Kid Added successfully" });
			else
				return BadRequest();
		}

		return Ok(new { Message = "User Added successfully" });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
			return Unauthorized(new BaseErrorResponse(401, "Invalid credentials"));

		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		var userRoles = await _userManager.GetRolesAsync(user);

		foreach (var role in userRoles)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, role));
		}

		var accessToken = await _authService.CreateTokenAsync(authClaims);
		var refreshToken = await _authService.GenerateRefreshToken(user);

		var userDto = new UserResponseDTO
		{
			Id = user.Id,
			FullName = user.FullName,
			Role = userRoles[0]
		};

		return Ok(new AuthResponse
		{
			AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
			RefreshToken = refreshToken.Token,
			AccessTokenExpiration = accessToken.ValidTo,
			User = userDto
		});
	}

	[HttpPost("refresh")]
	public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
	{
		var spec = new Specification<RefreshToken>()
		{
			Criteria = rt => rt.Token == request.RefreshToken,
			Includes = new()
			{
				rt => rt.User
			}
		};
		var refreshToken = await _unitOfWork.Repository<RefreshToken>().GetWithSpecAsync(spec);

		if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
			return Unauthorized(new BaseErrorResponse(401, "Invalid or expired refresh token"));

		var user = refreshToken.User;

		var userRoles = await _userManager.GetRolesAsync(user);

		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		foreach (var role in userRoles)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, role));
		}

		var newAccessToken = await _authService.CreateTokenAsync(authClaims);
		var newRefreshToken = await _authService.GenerateRefreshToken(user);

		// Revoke old refresh token
		refreshToken.IsRevoked = true;
		await _unitOfWork.CompleteAsync();

		var userDto = new UserResponseDTO
		{
			Id = user.Id,
			FullName = user.FullName,
			Role = userRoles[0]
		};

		return Ok(new AuthResponse
		{
			AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
			RefreshToken = newRefreshToken.Token,
			AccessTokenExpiration = newAccessToken.ValidTo,
			User = userDto
		});
	}

	[Authorize]
	[HttpGet("profile")]
	public async Task<IActionResult> GetProfile()
	{
		var user = await _userManager.GetUserAsync(User);

		if (user == null)
			return NotFound(new BaseErrorResponse(404, "User not found"));

		var roles = await _userManager.GetRolesAsync(user);

		return Ok(new UserResponseDTO
		{
			Id = user.Id,
			FullName = user.FullName,
			ProfilePictureUrl = user.ProfilePictureUrl,
			DailyUsageLimit = user.DailyUsageLimit,
			DailyUsageToday = user.DailyUsageToday,
			LastAccessDate = user.LastAccessDate,
			Role = roles[0]
		});
	}

	[HttpGet("roles")]
	public async Task<IActionResult> GetAllRoles()
	{
		return Ok(await _roleManager.Roles.ToListAsync());
	}
}

