namespace OnlineSchoolForKids.API.DTOs.Contents
{
	public class ContentToAddOrUpdate : ContentDto
	{
		public string Title { get; set; }
		public string TitleAr { get; set; }

		public string Description { get; set; }
		public string DescriptionAr { get; set; }
	}
}
