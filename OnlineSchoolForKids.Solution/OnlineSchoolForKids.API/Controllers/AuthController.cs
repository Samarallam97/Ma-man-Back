﻿using OnlineSchoolForKids.Core.Models;
using OnlineSchoolForKids.Core.ServiceInterfaces;

namespace OnlineSchoolForKids.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService=authService;
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

