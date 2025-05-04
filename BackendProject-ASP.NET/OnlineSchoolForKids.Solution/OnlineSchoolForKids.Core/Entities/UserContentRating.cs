namespace OnlineSchoolForKids.Core.Entities;

public class UserContentRating
{
    public int UserId { get; set; }
    public User User { get; set; }
	public int ContentId { get; set; }
    public Content Content { get; set; }
    public int Stars { get; set; }
    public DateTime RatingDate { get; set; }

}
