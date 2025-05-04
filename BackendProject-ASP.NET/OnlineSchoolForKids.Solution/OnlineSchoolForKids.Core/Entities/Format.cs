namespace OnlineSchoolForKids.Core.Entities;

public class Format : BaseEntity
{
    public string Name { get; set; }
	public ICollection<Content> Contents { get; set; } = new HashSet<Content>();

}
