namespace OnlineSchoolForKids.API.DTOs.Utilities;

public class EmailDto
{
	[Required]
	public string Subject { get; set; }

	[Required]
	public string To { get; set; }

	[Required]
	public string Body { get; set; }

	[Required]
	public bool IsHTML { get; set; }
}
