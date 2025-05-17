namespace OnlineSchoolForKids.API.DTOs
{
    public class ToDoDTO
    {
        [Required]
        public  int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
