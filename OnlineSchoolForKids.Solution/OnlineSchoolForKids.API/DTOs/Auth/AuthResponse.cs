namespace OnlineSchoolForKids.API.DTOs;

public class AuthResponse
{
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public DateTime AccessTokenExpiration { get; set; }
	public UserResponseDTO User { get; set; }
}
