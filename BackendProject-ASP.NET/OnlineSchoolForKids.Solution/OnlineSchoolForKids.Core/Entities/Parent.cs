namespace OnlineSchoolForKids.Core.Entities;
public class Parent : BaseEntity
{
    public User User { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Points { get; set; }

	public ICollection<Kid> Kids { get; set; } = new HashSet<Kid>();

}