using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
    public class NotificationController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private MicroSolutionsContext db = new MicroSolutionsContext();
		// GET: Notification
		public ActionResult PendingNotification()
        {
			var notifications = db.Notification.Where(n => n.IsNotified == false).ToList();
			var partNumbers = db.PartsForInvoice.ToList();
			var notificationViewModelList = new List<NotificationViewModel>();

			foreach (var item in notifications)
			{
				var notificationViewModel = new NotificationViewModel();
				notificationViewModel.Id = item.Id;
				notificationViewModel.InvoiceId = item.InvoiceId;
				notificationViewModel.IsNotified = false;
				notificationViewModel.ModifiedDate = DateTime.Now;
				notificationViewModel.UserName = WebSecurity.CurrentUserName;
				notificationViewModel.PartNumberId = item.PartNumberId;
				notificationViewModel.PartNumber = partNumbers.Where(p=>p.Id == item.PartNumberId).FirstOrDefault()?.PartNumber;
				notificationViewModel.NextNotificationDate = DateTime.Now;
				notificationViewModel.InvoiceNumber = db.Invoice.Where(i => i.Id == item.InvoiceId).FirstOrDefault().InvoiceNumber;
				notificationViewModelList.Add(notificationViewModel);
			}

			return View(notificationViewModelList);
        }

		public ActionResult SentNotification()
		{
			var notifications = db.Notification.Where(m=>m.IsNotified == true).ToList();
			var partNumbers = db.PartsForInvoice.ToList();
			var notificationViewModelList = new List<NotificationViewModel>();

			foreach (var item in notifications)
			{
				var notificationViewModel = new NotificationViewModel();
				notificationViewModel.Id = item.Id;
				notificationViewModel.InvoiceId = item.InvoiceId;
				notificationViewModel.IsNotified = false;
				notificationViewModel.ModifiedDate = DateTime.Now;
				notificationViewModel.UserName = WebSecurity.CurrentUserName;
				notificationViewModel.PartNumberId = item.PartNumberId;
				notificationViewModel.PartNumber = partNumbers.Where(p => p.Id == item.PartNumberId).FirstOrDefault()?.PartNumber;
				notificationViewModel.NextNotificationDate = DateTime.Now.AddMonths(1);
				notificationViewModel.InvoiceNumber = db.Invoice.Where(i => i.Id == item.InvoiceId).FirstOrDefault().InvoiceNumber;
				notificationViewModelList.Add(notificationViewModel);
			}

			return View(notificationViewModelList);
		}

		[Authorize(Roles = "Administrator,Host")]
		[HttpGet]
		public JsonResult Edit(string id)
		{
			var notifications = db.Notification.Where(m => m.Id == id).FirstOrDefault();
			var notificationVm = new NotificationViewModel();
			notificationVm.InvoiceNumber = db.Invoice.Find(notifications.InvoiceId).InvoiceNumber;
			notificationVm.PartNumber = db.PartsForInvoice.Find(notifications.PartNumberId).PartNumber;
			notificationVm.NextNotificationDate = DateTime.Now;
			notificationVm.Id = id;
			return Json(notificationVm, JsonRequestBehavior.AllowGet);

		}

		[Authorize(Roles = "Administrator,Host")]
		public JsonResult UpdateNotification(NotificationViewModel notificationVm)
		{
			try
			{
				var notificationDetails = new NotificationViewModel();
				var notofocationList = db.Notification.ToList();
				var notificationToUpdate = new Notification();
				if (notificationVm == null || !ModelState.IsValid)
					return Json(notificationVm);

				notificationToUpdate = db.Notification.Find(notificationVm.Id);

				if (notificationToUpdate == null)
					return Json(notofocationList);

				notificationToUpdate.IsNotified = false;
				notificationToUpdate.NextNotificationDate = notificationVm.NextNotificationDate;
				notificationToUpdate.ModifiedDate = DateTime.Now;
				notificationToUpdate.UserName = WebSecurity.CurrentUserName;

				if (TryUpdateModel(notificationToUpdate, new string[] { "IsNotified,NextNotificationDate,UserName,ModifiedDate" }))
				{
					db.Entry(notificationToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					notofocationList = db.Notification.ToList();
				}

				return Json(notofocationList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Notification ->  update[HTTPPOST]: " + ex.Message);
				throw;
			}
		}

	}
}