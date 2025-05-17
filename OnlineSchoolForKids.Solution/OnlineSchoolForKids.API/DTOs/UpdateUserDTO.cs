namespace OnlineSchoolForKids.API.DTOs;

public class UpdateUserDTO
{
	[Required]
	public string FirstName { get; set; }
	[Required]
	public string LastName { get; set; }
	[Required]
	public string PhoneNumber { get; set; }
}
