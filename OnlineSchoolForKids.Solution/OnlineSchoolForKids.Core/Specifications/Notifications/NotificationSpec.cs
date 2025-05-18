namespace OnlineSchoolForKids.Core.Specifications.Notifications;

public class NotificationSpec : Specification<Notification>
{
	public NotificationSpec(NotificationParams notificationParams)
  : base(
  e => (string.IsNullOrEmpty(notificationParams.Search) || e.Message.ToLower().Contains(notificationParams.Search))
		   || (string.IsNullOrEmpty(notificationParams.Search) || e.MessageAr.Contains(notificationParams.Search))
		   || (string.IsNullOrEmpty(notificationParams.Type) || e.Type == notificationParams.Type)
		   || (string.IsNullOrEmpty(notificationParams.UserId) || e.UserId == notificationParams.UserId)
		   ||  e.IsRead == notificationParams.IsRead)
	{
		ApplyPagination(notificationParams.PageSize, notificationParams.PageIndex);
		ApplySorting(notificationParams.SortDescending);
	}
	private void ApplyPagination(int? pageSize, int? pageIndex)
	{
		Skip = (pageIndex - 1) * pageSize;
		Take = pageSize;
	}

	private void ApplySorting(bool SortDesending)
	{
		if (SortDesending)
		{
			OrderByDesc = c => c.CreatedAt;
		}
		else
		{
			OrderBy = c => c.CreatedAt;
		}
	}
}
