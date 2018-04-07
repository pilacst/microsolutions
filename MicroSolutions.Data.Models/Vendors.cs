using System;
using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Data.Models
{
	public class Vendors
	{
		/// <summary>
		/// Vendor id.
		/// </summary>
		public virtual int Id { get; set; }

		/// <summary>
		/// Vendor or third party buyer name.
		/// </summary>
		[Display(Name = "Vendor name")]
		public virtual string VenderName { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }
	}
}
