namespace OnlineSchoolForKids.API.DTOs.Comments
{
	public class CommentDTO
	{
        public string Id { get; set; }
        public string ModuleId { get; set; }
		public string UserId { get; set; }
		public string Text { get; set; }
	}
}
