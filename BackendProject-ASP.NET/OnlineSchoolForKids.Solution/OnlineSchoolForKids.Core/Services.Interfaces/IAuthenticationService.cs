namespace OnlineSchoolForKids.Core.Services.Interfaces;

public interface IAuthenticationService
{
	Task<string> CreateTokenAsync(User user);
}
