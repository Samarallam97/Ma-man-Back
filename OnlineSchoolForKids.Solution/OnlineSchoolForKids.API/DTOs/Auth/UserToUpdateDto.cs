namespace OnlineSchoolForKids.API.DTOs.Auth;

public class UserToUpdateDto
{
	public string Id { get; set; }
	public string FullName { get; set; }
	public string? ProfilePictureUrl { get; set; }
	public int? DailyUsageLimit { get; set; } // Minutes
}
