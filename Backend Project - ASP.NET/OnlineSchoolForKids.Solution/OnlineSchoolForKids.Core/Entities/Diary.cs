namespace OnlineSchoolForKids.Core.Entities;

public class Diary : BaseEntity
{
	public string Content { get; set; }
	public DateTime CreationDate { get; set; }
	public int UserId { get; set; }
}
