namespace OnlineSchoolForKids.API.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
    }
}
