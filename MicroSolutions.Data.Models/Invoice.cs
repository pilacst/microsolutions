using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSolutions.Data.Models
{
	public class Invoice
	{
		public virtual int Id { get; set; }

		[StringLength(50)]
		[Required(ErrorMessage = "Invoice number required.")]
		public virtual string InvoiceNumber { get; set; }

		public virtual DateTime InvoiceDate { get; set; }

		[ForeignKey("Customer")]
		public virtual int CustomerId { get; set; } 

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual Customer Customer { get; set; }

		public virtual ICollection<PartsForInvoice> PartsForInvoice { get; set; }
	} 
}
