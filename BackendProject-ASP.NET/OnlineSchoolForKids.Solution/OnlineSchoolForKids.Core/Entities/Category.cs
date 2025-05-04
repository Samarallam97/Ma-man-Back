namespace OnlineSchoolForKids.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Content> Contents { get; set; } = new HashSet<Content>();
}
