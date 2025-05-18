using OnlineSchoolForKids.Core.Specifications.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface INotificationService
	{
		Task<bool> AddAsync(Notification notification);
		Task<bool> UpdateAsync(Notification notification);
		Task<bool> DeleteAsync(Notification notification);
		Task<Notification?> GetNotificationByIdAsync(string Id);
		Task<int> GetCountAsync(NotificationParams notificationParams);
		Task<IReadOnlyList<Notification>> GetAllNotificationsAsync(NotificationParams notificationParams);
	}
}
