using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class SupplierViewModel
	{
		public virtual int Id { get; set; }

		public virtual string SupplierName { get; set; }

		public virtual bool Status { get; set; }
	}
}