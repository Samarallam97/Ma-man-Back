


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
			expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:ExpireInDays"] !)),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return token;
	}

	public async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
	{
		var refreshToken = new RefreshToken
		{
			UserId = user.Id,
			Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
			Expires = DateTime.UtcNow.AddDays(7),
			Created = DateTime.UtcNow,
			IsRevoked = false
		};

		await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

		await _unitOfWork.CompleteAsync();

		return refreshToken;
	}
}
