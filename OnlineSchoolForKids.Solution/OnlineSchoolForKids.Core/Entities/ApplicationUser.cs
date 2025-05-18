using Microsoft.AspNetCore.Identity;
namespace OnlineSchoolForKids.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
	public int? DailyUsageLimit { get; set; } // Minutes
	public double DailyUsageToday { get; set; } // time elapsed
	public DateTime? LastAccessDate { get; set; }
	public string? ProfilePictureUrl { get; set; }
	public DateTime CreationDate { get; set; }
	public ICollection<HiddenModule> HiddenModules { get; set; } = new List<HiddenModule>();
}
