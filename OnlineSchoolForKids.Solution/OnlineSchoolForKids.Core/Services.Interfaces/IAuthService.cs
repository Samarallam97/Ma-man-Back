using OnlineSchoolForKids.Core.Models;

namespace OnlineSchoolForKids.Core.ServiceInterfaces;

public interface IAuthService
{
	Task<AuthModel> RegisterAsync(RegisterModel model);
	Task<AuthModel> LoginAsync(LoginModel model);
	Task<AuthModel> RefreshTokenAsync(string token);
	Task<bool> RevokeTokenAsync(string token);
	//Task<string> ForgetPasswordAsync(string email);

}
