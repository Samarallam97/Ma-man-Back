namespace OnlineSchoolForKids.API.DTOs;

public class ContentDTO
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Type { get; set; } // video, book, audio
    [Required]
    public string ContentUrl { get; set; }
    [Required]
    public string ModuleId { get; set; }
    [Required]
    public Module Module { get; set; }
    [Required]
    public string CreatedByAdminId { get; set; }
    [Required]
    public ApplicationUser CreatedByAdmin { get; set; }
    [Required]
    public List<AgeGroup> AgeGroups { get; set; }


}
