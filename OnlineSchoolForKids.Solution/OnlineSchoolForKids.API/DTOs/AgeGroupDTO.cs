namespace OnlineSchoolForKids.API.DTOs
{
    public class AgeGroupDTO
    {
        [Required]
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string Color { get; set; }
    }
}
