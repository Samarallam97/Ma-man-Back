
namespace OnlineSchoolForKids.Core.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; }
	public string NameAR { get; set; }
	public string Description { get; set; }
	public string DescriptionAR { get; set; }
    public string PicutureUrl { get; set; }
    public string Color { get; set; }
    public string? CreatedByAdminId { get; set; } 
	public ApplicationUser? CreatedByAdmin { get; set; } 
	public List<Module> Modules { get; set; }
}
