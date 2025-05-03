namespace OnlineSchoolForKids.API.DTOs
{
    public class ParentDTO
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public ICollection<Kid> Kids { get; set; } = new List<Kid>();   
    }
}
