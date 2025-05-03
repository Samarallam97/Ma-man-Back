namespace OnlineSchoolForKids.API.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ContentId { get; set; }
    }
}
