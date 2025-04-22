namespace OnlineSchoolForKids.Services;

public class AuthenticationService : IAuthenticationService
{
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;

	public AuthenticationService(UserManager<User> userManager , IConfiguration configuration)
    {
		_userManager=userManager;
		_configuration=configuration;
	}
    public async Task<string> CreateTokenAsync(User user )
	{
		var userRoles = await _userManager.GetRolesAsync(user);

		var authClaims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub , user.UserName!),
				new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
			};

		authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

		var token = new JwtSecurityToken
			(
				issuer: _configuration["JWT:Issuer"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpireInDays"]!)),
				claims: authClaims,
				signingCredentials: new SigningCredentials
					(
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)),
						SecurityAlgorithms.HmacSha256
					)
			);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
