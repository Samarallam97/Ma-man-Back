using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineSchoolForKids.Core.ServiceInterfaces;

public interface IAuthService
{
	Task<JwtSecurityToken> CreateTokenAsync(List<Claim> claims);
	Task<RefreshToken> GenerateRefreshToken(ApplicationUser user);
}
