using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolForKids.API.DTOs;

public class RegisterDto
{
	[Required]
	public string FullName { get; set; }
	
	[Required]
	public string Username { get; set; }
	
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[Required]
	public string Password { get; set; }

	public string Role { get; set; } = "User";
}
