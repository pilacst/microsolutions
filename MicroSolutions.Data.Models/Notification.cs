using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSolutions.Data.Models
{
	public class Notification
	{
		[Key]
		public virtual string Id { get; set; }

		[ForeignKey("Invoice")]
		public virtual int InvoiceId { get; set; }

		[ForeignKey("PartsForInvoice")]
		public virtual string PartNumberId { get; set; }

		public virtual bool IsNotified { get; set; }

		public virtual DateTime NextNotificationDate { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }

		public virtual Invoice Invoice { get; set; }

		public virtual PartsForInvoice PartsForInvoice { get; set; }
	}
}
