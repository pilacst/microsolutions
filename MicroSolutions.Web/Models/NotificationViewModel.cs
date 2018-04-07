using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class NotificationViewModel
	{
		public virtual string Id { get; set; }

		public virtual int InvoiceId { get; set; }

		public virtual string InvoiceNumber { get; set; }

		public virtual string PartNumberId { get; set; }

		public virtual string PartNumber { get; set; }

		public virtual bool IsNotified { get; set; }

		public virtual DateTime NextNotificationDate { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }
	}
}