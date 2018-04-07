using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSolutions.Data.Models
{
	public class Supplier
	{
		public virtual int Id { get; set; }

		public virtual string SupplierName { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }
	}
}
