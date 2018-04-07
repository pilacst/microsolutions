using Quartz;

namespace MicroSolutions.Task.ImportDataForEmailAlert
{
	public class NotificationTableUpdateJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			var updateNotificationTable = new UpdateNotificationData();
			updateNotificationTable.UpdatePendingNotifications();
		}
	}
}
