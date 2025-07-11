using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using OnlineSchoolForKids.API.DTOs.Auth;
using OnlineSchoolForKids.Core.Models;
using OnlineSchoolForKids.Core.ServiceInterfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly IConfiguration _configuration;

	public AuthController(IAuthService authService , IConfiguration configuration)
	{
		_authService=authService;
		_configuration=configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterModel model)
	{

		var result = await _authService.RegisterAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);

		SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

		return Ok(result);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		var result = await _authService.LoginAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);

		if (!string.IsNullOrEmpty(result.RefreshToken))
			SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

		return Ok(result);
	}

	[HttpPost("external-google-login")]
	public async Task<IActionResult> ExternalLogin([FromBody] GoogleTokenDto tokenDto)
	{
		if (string.IsNullOrWhiteSpace(tokenDto.IdToken))
			return BadRequest("ID token is required.");

		GoogleJsonWebSignature.Payload payload;

		try
		{
			payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.IdToken, new GoogleJsonWebSignature.ValidationSettings
			{
				Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
			});
		}
		catch (Exception ex)
		{
			return BadRequest($"Invalid Google token: {ex.Message}");
		}

		var model = new ExternalAuthModel
		{
			Email = payload.Email,
			Name = payload.Name,
			PictureUrl = payload.Picture,
			Provider = "Google",
			ProviderUserId = payload.Subject // Google's unique user ID
		};

		var result = await _authService.ExternalLoginAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);

		SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

		return Ok(result);
	}

	[HttpPost("forget-password")]
	public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordModel model )
	{
		var result = await _authService.ForgetPasswordAsync(model);

		return Ok(result);
	}

	[HttpPost("reset-password")]

	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
	{
		var result = await _authService.ResetPasswordAsync(model);

		if(!result.IsResetted)
			return BadRequest(result);

		return Ok(result);
	}


		[HttpGet("refresh-token")]
	public async Task<IActionResult> RefreshToken()
	{
		var refreshToken = Request.Cookies["refreshToken"];

		var result = await _authService.RefreshTokenAsync(refreshToken);

		if (!result.IsAuthenticated)
			return BadRequest(result);

		SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

		return Ok(result);
	}

	[HttpPost("revoke-token")]
	public async Task<IActionResult> RevokeToken()
	{
		var token = Request.Cookies["refreshToken"];

		if (string.IsNullOrEmpty(token))
			return BadRequest("Token is required!");

		var result = await _authService.RevokeTokenAsync(token);

		if (!result)
			return BadRequest("Token is invalid!");

		return Ok();
	}



	/// ////////////////////////////////////////////////////////////////////////// Private Methods
	private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = expires.ToLocalTime(),
				Secure = true,
				IsEssential = true,
				SameSite = SameSiteMode.None
			};

			Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
		}
}

