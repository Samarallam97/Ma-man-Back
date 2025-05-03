namespace OnlineSchoolForKids.API.DTOs;

public class CategoryDTO
{ 
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

}
