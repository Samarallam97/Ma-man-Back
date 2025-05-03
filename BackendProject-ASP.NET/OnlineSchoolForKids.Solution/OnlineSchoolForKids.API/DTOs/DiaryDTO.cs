namespace OnlineSchoolForKids.API.DTOs
{
    public class DiaryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
