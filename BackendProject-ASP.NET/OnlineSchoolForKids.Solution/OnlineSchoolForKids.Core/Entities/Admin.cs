namespace OnlineSchoolForKids.Core.Entities;

public class Admin : BaseEntity
{
    public DateTime DateOfBirth { get; set; }
    public DateTime HiringDate { get; set; }
    public double Salary { get; set; }
    public User User { get; set; }
    public ICollection<Content> Contents { get; set; } = new HashSet<Content>();


}
