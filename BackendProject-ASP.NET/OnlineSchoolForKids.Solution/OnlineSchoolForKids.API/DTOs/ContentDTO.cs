namespace OnlineSchoolForKids.API.DTOs;

public class ContentDTO
{
    public int Id { get; set; }

    [Required]
	public string Title { get; set; }
	[Required]
	public string Description { get; set; }
	[Required]
	public string URL { get; set; }

	[Required]
	public int FormatId { get; set; }

	[Required]
	public int CategoryId { get; set; }

	[Required]
	public int AdminId { get; set; }

    public int AverageRate { get; set; }

}
