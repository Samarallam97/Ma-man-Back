namespace OnlineSchoolForKids.Core.Entities;

public class Adult : BaseEntity
{
    public DateTime DateOfBirth { get; set; }
    public int TimeLimitWithMinutes { get; set; }
    public int Points { get; set; }
    public int? AgeGroupId { get; set; }

}
