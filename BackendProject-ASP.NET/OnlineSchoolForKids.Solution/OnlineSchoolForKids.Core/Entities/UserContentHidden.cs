
namespace OnlineSchoolForKids.Core.Entities;

public class UserContentHidden
{
	public int UserId { get; set; }
	public User User { get; set; }
	public int ContentId { get; set; }
	public Content Content { get; set; }
}
