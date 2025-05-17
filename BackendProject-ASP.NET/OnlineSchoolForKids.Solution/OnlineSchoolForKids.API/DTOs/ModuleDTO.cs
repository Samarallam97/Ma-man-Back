namespace OnlineSchoolForKids.API.DTOs
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public string CreatedByAdminId { get; set; }
        [Required]
        public ApplicationUser CreatedByAdmin { get; set; }
        [Required]
        public double AverageRating { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }
        [Required]
        public List<Content> Contents { get; set; }
    }
}
