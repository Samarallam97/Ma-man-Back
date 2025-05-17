namespace OnlineSchoolForKids.API.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string? CreatedByAdminId { get; set; }
    [Required]
    public ApplicationUser CreatedByAdmin { get; set; }
    [Required]
    public List<Module> Modules { get; set; }

}
