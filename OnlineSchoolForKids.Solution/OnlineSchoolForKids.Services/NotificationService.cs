using OnlineSchoolForKids.Core.Specifications.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Services
{
	public class NotificationService : INotificationService
	{

		private readonly IUnitOfWork _unitOfWork;

		public NotificationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public async Task<bool> AddAsync(Notification notification)
		{
			await _unitOfWork.Repository<Notification>().AddAsync(notification);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> UpdateAsync(Notification notification)
		{

			_unitOfWork.Repository<Notification>().Update(notification);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> DeleteAsync(Notification notification)
		{
			_unitOfWork.Repository<Notification>().Delete(notification);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<IReadOnlyList<Notification>> GetAllNotificationsAsync(NotificationParams notificationParams)
		{
			var spec = new NotificationSpec(notificationParams);

			var notifications = await _unitOfWork.Repository<Notification>().GetAllWithSpecAsync(spec);

			return notifications;
		}

		public async Task<Notification?> GetNotificationByIdAsync(string Id)
		{
			return await _unitOfWork.Repository<Notification>().GetByIdAsync(Id);
		}

		public async Task<int> GetCountAsync(NotificationParams notificationParams)
		{
			var spec = new NotificationSpec(notificationParams);

			var count = (await _unitOfWork.Repository<Notification>().GetAllWithSpecAsync(spec)).Count();

			return count;
		}

	}

}