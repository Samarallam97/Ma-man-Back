namespace OnlineSchoolForKids.Core.Entities;

public class AgeGroup : BaseEntity
{
	public string Name { get; set; }
	public ICollection<Content> Content { get; set; } = new HashSet<Content>();
    public ICollection<Adult> Adults { get; set; } = new HashSet<Adult>();
	public ICollection<Kid> Kids { get; set; } = new HashSet<Kid>();
}
