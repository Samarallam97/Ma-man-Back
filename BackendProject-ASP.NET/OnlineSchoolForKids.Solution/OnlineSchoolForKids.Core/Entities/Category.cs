

using System.Reflection;

namespace OnlineSchoolForKids.Core.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string? CreatedByAdminId { get; set; } 
	public ApplicationUser? CreatedByAdmin { get; set; } 
	public List<Module> Modules { get; set; }
}
