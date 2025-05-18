namespace OnlineSchoolForKids.API.DTOs.Contents
{
	public class ContentDto
	{
        public string Id { get; set; }
        public string Type { get; set; } // video, book, audio
		public string ContentUrl { get; set; }
		public string ModuleId { get; set; }
		public string? CreatedByAdminId { get; set; }
		public List<string> AgeGroupsIds { get; set; }
	}
}
