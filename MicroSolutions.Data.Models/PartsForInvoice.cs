using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSolutions.Data.Models
{
	public class PartsForInvoice
	{
		[Key]
		public virtual string Id { get; set; } 

		[ForeignKey("ItemType")]
		public virtual int ItemTypeId { get; set; }

		public string PartNumber { get; set; }

		[ForeignKey("Vendor")]
		public int VendorId { get; set; }

		public string SerialNumber { get; set; }

		public virtual int Quantity { get; set; }

		[ForeignKey("Supplier")]
		public int SupplierId { get; set; }

		public virtual string Description { get; set; }

		[ForeignKey("ExpirationPeriod")]
		public int ExpirationPeriodId { get; set; }

		public DateTime StartingDate { get; set; }

		public DateTime EndDate { get; set; }

		public virtual bool Status { get; set; }

		[ForeignKey("Invoice")]
		public virtual int Invoice_Id { get; set; }

		public virtual string UserName { get; set; }

		public Vendors Vendor { get; set; }

		public ExpirationPeriod ExpirationPeriod { get; set; }

		public ItemType ItemType { get; set; }

		public virtual Invoice Invoice { get; set; }

		public virtual Supplier Supplier { get; set; }
	}
}
