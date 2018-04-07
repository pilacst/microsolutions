using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class HomeVieModel
	{
		public virtual int TotalInvoices { get; set; }

		public virtual int TotalExpiredParts { get; set; }

		public virtual int TotalExpiredItemOnToday { get; set; }

		public virtual int TotalItemsExpiredWithinNextThreeMonths { get; set; }
	}
}