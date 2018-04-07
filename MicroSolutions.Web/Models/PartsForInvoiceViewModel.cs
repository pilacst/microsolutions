using MicroSolutions.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class PartsForInvoiceViewModel
	{
		public virtual string Id { get; set; }

		public virtual int InvoiceId { get; set; }

		[ForeignKey("ItemType")]
		public virtual int ItemTypeId { get; set; }

		public string PartNumber { get; set; }

		[ForeignKey("Vendor")]
		public int VendorId { get; set; }

		public string SerialNumber { get; set; }

		public virtual int SupplierId { get; set; }

		public virtual int Quantity { get; set; }

		public virtual string Description { get; set; }

		[ForeignKey("ExpirationPeriod")]
		public int ExpirationPeriodId { get; set; }

		public string StartingDate { get; set; }

		public string EndDate { get; set; }

		public virtual bool Status { get; set; }

		public virtual bool IsNew { get; set; }

		public Vendors Vendor { get; set; }

		public ExpirationPeriod ExpirationPeriod { get; set; }

		public ItemType ItemType { get; set; }

		public Supplier Supplier { get; set; }

		public virtual Invoice Invoice { get; set; }
	}
}