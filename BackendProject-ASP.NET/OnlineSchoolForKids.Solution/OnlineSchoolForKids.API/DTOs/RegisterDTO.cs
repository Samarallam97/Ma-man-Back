using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolForKids.API.DTOs;

public class RegisterDTO
{
	public string FirstName { get; set; }
	public string LastName { get; set; } 
	public string? UserName { get; set; }
	public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
	public string Password { get; set; }
}
