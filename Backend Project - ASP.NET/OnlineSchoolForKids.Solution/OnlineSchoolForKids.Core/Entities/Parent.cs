namespace OnlineSchoolForKids.Core.Entities;

public class Parent : BaseEntity
{
    public DateTime DateOfBirth { get; set; }
    public int Points { get; set; }

	//public ICollection<Kid> Kids { get; set; } = new List<Kid>();
}
