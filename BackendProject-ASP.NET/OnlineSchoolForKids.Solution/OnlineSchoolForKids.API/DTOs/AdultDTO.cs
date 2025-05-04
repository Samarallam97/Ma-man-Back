namespace OnlineSchoolForKids.API.DTOs
{
    public class AdultDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int TimeLimitWithMinutes { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int? AgeGroupId { get; set; }
    }
}
