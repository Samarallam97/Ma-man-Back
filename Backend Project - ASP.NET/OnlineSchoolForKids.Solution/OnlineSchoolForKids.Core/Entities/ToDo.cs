namespace OnlineSchoolForKids.Core.Entities;

public class ToDo : BaseEntity
{
    public string Content { get; set; }
	public DateTime CreationDate { get; set; }
	public bool Status { get; set; }
    public int UserId { get; set; }
}