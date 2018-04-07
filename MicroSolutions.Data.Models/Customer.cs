using System;
using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Data.Models
{
	public class Customer
	{
		public virtual int Id { get; set; }

		[Display(Name = "Customer name")]
		[StringLength(150)]
		public virtual string CustomerName { get; set; }

		[Display(Name = "Contact person name")]
		[StringLength(150)]
		public virtual string ContactPersonName { get; set; }

		[Display(Name = "Contact number")]
		[StringLength(15)]
		public virtual string ContactNumber { get; set; }

		[Display(Name = "Customer address")]
		[StringLength(350)]
		public virtual string Address { get; set; }

		[Display(Name = "Memo")]
		public virtual string Memorandum { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; } 
	}
}
