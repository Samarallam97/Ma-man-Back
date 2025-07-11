using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolForKids.API.DTOs;

public class RegisterDto
{
	[Required]
	public string FullName { get; set; }
	
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	public string Password { get; set; }

	[Required]
	[Compare("Password")]
	public string ConfirmPassword { get; set; }

	public string Role { get; set; }

    public string? ParentId { get; set; }
}
