namespace OnlineSchoolForKids.API.DTOs.Notifications
{
	public class NotificationDTO
	{
        public string Id { get; set; }
        public string UserId { get; set; }
		public string Message { get; set; }
		public string MessageAr { get; set; }
		public bool IsRead { get; set; } = false;
		public string Type { get; set; }
	}
}
