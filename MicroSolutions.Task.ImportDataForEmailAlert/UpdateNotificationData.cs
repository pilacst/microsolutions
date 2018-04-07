using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using NLog;
using System;
using System.Linq;

namespace MicroSolutions.Task.ImportDataForEmailAlert
{
    public class UpdateNotificationData
	{
		private MicroSolutionsContext db = new MicroSolutionsContext();
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public void UpdatePendingNotifications()
		{
			try
			{
				var notificationList = db.Notification.ToList();
				var warningPeriod = db.EmailSettings.ToList().FirstOrDefault().WarningPeriod;
				var date = DateTime.Now.AddMonths(warningPeriod).ToString("yyyy-MM-dd");
				var pendingList = db.PartsForInvoice.ToList().Where(prts => prts.EndDate.ToString("yyyy-MM-dd") == date);				
	
				foreach (var item in pendingList)
				{
					var alreadyExist = notificationList.Where(nl => nl.PartNumberId == item.Id).ToList().Count;
					if (notificationList != null && alreadyExist > 0)
						continue;

					var notification = new Notification();
					notification.Id = Guid.NewGuid().ToString();
					notification.InvoiceId = item.Invoice_Id;
					notification.IsNotified = false;
					notification.ModifiedDate = DateTime.Now;
					notification.NextNotificationDate = DateTime.Now;
					notification.PartNumberId = item.Id;
					notification.UserName = "Task";

					db.Notification.Add(notification);
					db.Entry(notification).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
				}
			}
			catch (Exception error)
			{
				logger.Log(LogLevel.Error, "Exception occurred in update notification table task " + error.Message);
				throw;
			}
		}
	}
}
