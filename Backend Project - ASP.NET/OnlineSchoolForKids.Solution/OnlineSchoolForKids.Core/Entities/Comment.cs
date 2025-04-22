namespace OnlineSchoolForKids.Core.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; }
	public DateTime Date { get; set; }
	public int UserId { get; set; }
	public int ContentId { get; set; }
}
