namespace OnlineSchoolForKids.API.DTOs.Modules
{
	public class ModuleDTO
	{
		public string Id { get; set; }
		public string CategoryId { get; set; }
		public string? CreatedByAdminId { get; set; }
		public double AverageRating { get; set; }
		public string Color { get; set; }
		public string PicutureUrl { get; set; }
	}
}
