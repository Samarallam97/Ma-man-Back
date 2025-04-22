namespace OnlineSchoolForKids.Core.Entities;

public class Role : IdentityRole<int>
{

    public Role()
    {
        
    }
    public Role(string roleName) : base(roleName)
	{
	}
}
