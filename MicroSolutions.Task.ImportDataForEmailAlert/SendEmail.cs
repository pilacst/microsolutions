using MicroSolutions.DAL;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;

namespace MicroSolutions.Task.ImportDataForEmailAlert
{
    public class SendEmail
	{
		private MicroSolutionsContext db = new MicroSolutionsContext();
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public void SendEmailsToAdmin()
		{
			try
			{
				var emailConfiguration = db.EmailSettings.ToList().FirstOrDefault();
				string smtpAddress = emailConfiguration.SmtpAddress;//"smtp.gmail.com";
				string password = emailConfiguration.FromEmailPassword; //"3139808@pila";

				string emailFrom = emailConfiguration.FromEmail; //"nayanajith.pilapitiya@gmail.com";
				string emailTo = emailConfiguration.ToEmail.ToString();
				int portNumber = emailConfiguration.PortNumber; //587;
				string StrContent = "";
				string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content/files/email_template.html");
				StreamReader reader = new StreamReader(filePath);
				string readFile = reader.ReadToEnd();
				StrContent = readFile;
				StringBuilder tableBody = new StringBuilder();
				var notificationList = db.Notification.ToList();
				var invoiceList = db.Invoice.ToList();

				var notification = from not in db.Notification.ToList()
								   join inv in db.Invoice.ToList() on not.InvoiceId equals inv.Id
								   join part in db.PartsForInvoice.ToList() on not.PartNumberId equals part.Id
								   where (not.IsNotified == false) && (not.NextNotificationDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
								   select not;

				if (notification.ToList().Count <= 0)
					return;

				//var invocies = db.Invoice.ToList().Where(inv => inv.InvoiceDate < DateTime.Now.AddMonths(3) && inv.Status == 1 && (inv.ExpirationPeriodId != 0 || inv.ExpirationPeriodId != 1000));
				string table = "<Table  class=" + "zui-table" + "><Tr><Th>Invoice number</Th><Th>Part number</Th><Th>Warranty end date</Th></Tr>{0}</Table>";

				foreach (var item in notification.ToList())
				{
					tableBody.Append("<tr><td>" + item?.Invoice?.InvoiceNumber + "</td><td>" + item?.PartsForInvoice?.PartNumber + "</td><td>" + item?.PartsForInvoice?.EndDate + "</td></tr>");
				}

				string logTable = string.Format(table, tableBody);
				StrContent = StrContent.Replace("[ProductList]", logTable);

				using (MailMessage mail = new MailMessage())
				{
					mail.From = new MailAddress(emailFrom);
					mail.Subject = "Expiration system alert"; //subject;
					mail.Body = StrContent;
					mail.IsBodyHtml = true;
					mail.To.Add(emailTo);

					using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
					{
						smtp.UseDefaultCredentials = false;
						smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
						smtp.Credentials = new NetworkCredential(emailFrom, password);
						smtp.EnableSsl = true;

						try
						{
							smtp.Send(mail);
						}
						catch (Exception error)
						{
							logger.Log(LogLevel.Error, "Exception occurred in send email task " + error.Message);
							throw;
						}
					}
				}

				foreach (var item in notification)
				{
					if (db.Notification.Where(n => n.Id == item.Id).FirstOrDefault() != null)
					{
						db.Notification.Where(n => n.Id == item.Id).FirstOrDefault().IsNotified = true;
						db.SaveChanges();
					}
				}

			}
			catch (Exception error)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Send email task: " + error.Message);
				throw;
			}
		}
	}
}
