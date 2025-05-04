namespace OnlineSchoolForKids.Core.Entities;

public class Content : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
	public string URL { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AverageRate { get; set; }
	public int FormatId { get; set; }
    public Format Format { get; set; }
    public int? CategoryId { get; set; }
    public Category Category { get; set; }
    public int? AdminId { get; set; }
    public Admin Admin { get; set; }

	public ICollection<AgeGroup> AgeGroups { get; set; } = new HashSet<AgeGroup>();
	public ICollection<UserContentRating> UsersRated { get; set; } = new HashSet<UserContentRating>();
	public ICollection<UserContentHidden> UsersHidden { get; set; } = new HashSet<UserContentHidden>();
}
