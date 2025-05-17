

namespace OnlineSchoolForKids.Core.Entities;

public class ParentChild : BaseEntity
{
	public string ParentId { get; set; } //  AspNetUsers
    public ApplicationUser Parent { get; set; }
    public string ChildId { get; set; } //  AspNetUsers
	public ApplicationUser Child { get; set; }

}
