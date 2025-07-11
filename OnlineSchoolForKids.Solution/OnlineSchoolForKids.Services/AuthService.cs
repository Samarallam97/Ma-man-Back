using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineSchoolForKids.Core.Models;
using OnlineSchoolForKids.Core.Specifications;
using System;
using System.Web;

namespace OnlineSchoolForKids.Service;

public class AuthService : IAuthService
{
	private readonly IConfiguration _configuration;

	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration=configuration;
	}

	public async Task<AuthModel> RegisterAsync(RegisterModel model)
	{

		if (!await _roleManager.RoleExistsAsync(model.Role))
			return new AuthModel { Message = "Role not exist!" };

		if (await _userManager.FindByEmailAsync(model.Email) is not null)
			return new AuthModel { Message = "Email is already registered!" };

		if (model.Role == "Kid" && model.ParentId is null)
			return new AuthModel { Message =  "Can't add a kid without parent id" };

		var user = new ApplicationUser
		{
			FullName = model.FullName,
			UserName = model.Email,
			Email = model.Email,
			CreationDate = DateTime.UtcNow.ToLocalTime(),
			LastAccessDate = DateTime.UtcNow.ToLocalTime()
		};

		var result = await _userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
			return new AuthModel { Message = string.Join(",", result.Errors) };


		await _userManager.AddToRoleAsync(user, model.Role);


		//if (model.Role == "Kid")
		//{

		//	var parentChildLink = new ParentChild
		//	{
		//		ParentId = model.ParentId,
		//		ChildId = user.Id
		//	};

		//	await _unitOfWork.Repository<ParentChild>().AddAsync(parentChildLink);

		//	var count = await _unitOfWork.CompleteAsync();
		//	if (count <= 0)
		//		return BadRequest(new BaseErrorResponse(400, "Failed to link kid to parent"));

		//	return Ok(new { Message = "Kid added successfully" });
		//}


		var jwtSecurityToken = await CreateJwtToken(user);

		var refreshToken = GenerateRefreshToken();
		user.RefreshTokens?.Add(refreshToken);
		await _userManager.UpdateAsync(user);

		return new AuthModel
		{
			AccessTokenExpiration = jwtSecurityToken.ValidTo.ToLocalTime(),
			IsAuthenticated = true,
			AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
			RefreshToken = refreshToken.Token,
			RefreshTokenExpiration = refreshToken.ExpiresAt
		};
	}

	public async Task<AuthModel> LoginAsync(LoginModel model)
	{
		var authModel = new AuthModel();

		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
		{
			authModel.Message = "Email or Password is incorrect!";
			return authModel;
		}

		var jwtSecurityToken = await CreateJwtToken(user);

		var roles = await _userManager.GetRolesAsync(user);

		authModel.IsAuthenticated = true;
		authModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		authModel.AccessTokenExpiration = jwtSecurityToken.ValidTo.ToLocalTime();

		if (user.RefreshTokens.Any(t => t.IsActive))
		{
			var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
			authModel.RefreshToken = activeRefreshToken.Token;
			authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresAt;
		}
		else
		{
			var refreshToken = GenerateRefreshToken();
			authModel.RefreshToken = refreshToken.Token;
			authModel.RefreshTokenExpiration = refreshToken.ExpiresAt;
			user.RefreshTokens.Add(refreshToken);
			await _userManager.UpdateAsync(user);
		}

		return authModel;
	}

	public async Task<AuthModel> RefreshTokenAsync(string token)
	{
		var authModel = new AuthModel();

		var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

		if (user == null)
		{
			authModel.Message = "Invalid token";
			return authModel;
		}

		var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

		if (!refreshToken.IsActive)
		{
			authModel.Message = "Inactive token";
			return authModel;
		}

		refreshToken.RevokedAt = DateTime.UtcNow.ToLocalTime();

		var newRefreshToken = GenerateRefreshToken();
		user.RefreshTokens.Add(newRefreshToken);
		await _userManager.UpdateAsync(user);

		var jwtToken = await CreateJwtToken(user);
		authModel.IsAuthenticated = true;
		authModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
		authModel.AccessTokenExpiration = jwtToken.ValidTo.ToLocalTime();
		authModel.RefreshToken = newRefreshToken.Token;
		authModel.RefreshTokenExpiration = newRefreshToken.ExpiresAt;

		return authModel;
	}

	public async Task<bool> RevokeTokenAsync(string token)
	{
		var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

		if (user == null)
			return false;

		var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

		if (!refreshToken.IsActive)
			return false;

		refreshToken.RevokedAt = DateTime.UtcNow.ToLocalTime();

		await _userManager.UpdateAsync(user);

		return true;
	}



	/// ////////////////////////////////////////////////////////////////////////// Private Methods

	private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
	{

		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));

		var token = new JwtSecurityToken
		(
			audience: _configuration["JWT:Audience"],
			issuer: _configuration["JWT:Issuer"],
			expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:AccessTokenExpireInMinutes"]!)),
			claims: await GenerateUserClaimsAsync(user),
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return token;

	}

	private RefreshToken GenerateRefreshToken()
	{
		var randomNumber = new byte[64];

		using var generator = new RNGCryptoServiceProvider();

		generator.GetBytes(randomNumber);


		return new RefreshToken
		{
			Token = Convert.ToBase64String(randomNumber),
			ExpiresAt = DateTime.UtcNow.ToLocalTime().AddDays( double.TryParse(_configuration["JWT:RefreshTokenExpireInDays"], out var days) ? days : 10),
			CreatedAt = DateTime.UtcNow.ToLocalTime()
		};
	}

	private async Task<List<Claim>> GenerateUserClaimsAsync(ApplicationUser user)
	{

		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
			new Claim("userId", user.Id),
			new Claim("fullName", user.FullName ?? ""),
			new Claim("email", user.Email ?? ""),
			new Claim("profilePictureUrl", user.ProfilePictureUrl ?? ""),
			new Claim("dailyUsageLimit", user.DailyUsageLimit.ToString()),
			new Claim("dailyUsageToday", user.DailyUsageToday.ToString()),
			new Claim("lastAccessDate", user.LastAccessDate.ToString()),
			new Claim("role", roles[0]) 
		};

		return claims;
	}

	
}
