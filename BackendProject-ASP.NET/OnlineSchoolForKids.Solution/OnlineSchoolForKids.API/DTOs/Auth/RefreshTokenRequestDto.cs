using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolForKids.API.DTOs;

public class RefreshTokenRequestDto
{
	[Required]
	public string RefreshToken { get; set; }
}
