


using OnlineSchoolForKids.Core.Specifications;

namespace OnlineSchoolForKids.Service;

public class AuthService : IAuthService
{
	private readonly IConfiguration _configuration;
	private readonly IUnitOfWork _unitOfWork;

	public AuthService(IConfiguration configuration , IUnitOfWork unitOfWork)
	{
		_configuration=configuration;
		_unitOfWork=unitOfWork;
	}
	public async Task<JwtSecurityToken> CreateTokenAsync(List<Claim> authClaims)
	{

		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"] !));

		var token = new JwtSecurityToken
		(
			audience: _configuration["JWT:Audience"],
			issuer: _configuration["JWT:Issuer"],
			expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:AccessTokenExpireInMinutes"] !)),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return token;
	}

	public async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
	{
		var tokenBytes = RandomNumberGenerator.GetBytes(64);
		var token = Convert.ToBase64String(tokenBytes);

		var expiresInDays = double.TryParse(_configuration["JWT:RefreshTokenExpireInDays"], out var days)
			? days
			: 7; // fallback to 7 days if config is missing or invalid

		var refreshToken = new RefreshToken
		{
			UserId = user.Id,
			Token = token,
			CreatedAt = DateTime.UtcNow,
			ExpiresAt = DateTime.UtcNow.AddDays(expiresInDays),
		};

		var oldTokens = await _unitOfWork.Repository<RefreshToken>().GetAllWithSpecAsync(new Specification<RefreshToken>
		{
			Criteria = r => r.UserId == user.Id && r.RevokedAt == null && r.ExpiresAt > DateTime.UtcNow
		});

		foreach (var oldToken in oldTokens)
		{
			oldToken.RevokedAt = DateTime.UtcNow;
		}


		await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
		await _unitOfWork.CompleteAsync();

		return refreshToken;
	}

}
