using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolForKids.API.DTOs;

public class AssignRoleDto
{
	[Required]
	public string UserId { get; set; }
	[Required]
	public string Role { get; set; }
}
