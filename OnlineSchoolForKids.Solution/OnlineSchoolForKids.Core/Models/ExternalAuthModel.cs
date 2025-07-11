
namespace OnlineSchoolForKids.Core.Models;

public class ExternalAuthModel
{
	public string Email { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string PictureUrl { get; set; } = string.Empty;
	public string Provider { get; set; } = string.Empty;
	public string ProviderUserId { get; set; } = string.Empty;
}