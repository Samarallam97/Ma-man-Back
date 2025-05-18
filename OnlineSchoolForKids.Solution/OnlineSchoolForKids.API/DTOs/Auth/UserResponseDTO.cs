namespace OnlineSchoolForKids.API.DTOs;

public class UserResponseDTO
{
	public string Id { get; set; }
	public string FullName { get; set; }
	public string? ProfilePictureUrl { get; set; }
	public int? DailyUsageLimit { get; set; } // Minutes
	public double DailyUsageToday { get; set; } // time elapsed
	public DateTime? LastAccessDate { get; set; }
    public string Role { get; set; }

}
