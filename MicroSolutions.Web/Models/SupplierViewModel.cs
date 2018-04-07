using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class SupplierViewModel
	{
		public virtual int Id { get; set; }

        [Display(Name = "Supplier name")]
        [Required(ErrorMessage = "Supplier name required.")]
		public virtual string SupplierName { get; set; }

		public virtual bool Status { get; set; }
	}
}