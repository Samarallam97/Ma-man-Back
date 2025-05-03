namespace OnlineSchoolForKids.Core.Entities;

public class User : IdentityUser<int> 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PictureURL { get; set; }
    public DateTime CreationDate { get; set; }

}
