using MicroSolutions.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSolutions.Web.Models
{
	public class InvoiceViewModel
	{
		public virtual int Id { get; set; }

		[Display(Name = "Invoice number")]
		[StringLength(50)]
		[Required (ErrorMessage = "Invoice number required.")]
		public virtual string InvoiceNumber { get; set; }

		[Display(Name = "Invoice date")]
		public virtual string InvoiceDate { get; set; }

		public virtual bool Status { get; set; }

		[Display(Name = "Customer")]
		[ForeignKey("Customer")]
		public virtual int CustomerId { get; set; }

		[Display(Name = "Customer")]
		public virtual Customer Customer { get; set; }

		public virtual ICollection<PartsForInvoiceViewModel> PartsForInvoiceViewModel { get; set; }
	}
}