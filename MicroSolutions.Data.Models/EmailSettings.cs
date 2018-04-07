using System;
using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Data.Models
{
	public class EmailSettings
	{
		public virtual int Id { get; set; }

		[Display(Name ="From email")]
		public virtual string FromEmail { get; set; }

		[Display(Name = "From email password")]
		public virtual string FromEmailPassword { get; set; }

		[Display(Name = "To email")]
		public virtual string ToEmail { get; set; }

		[Display(Name = "Warning period")]
		public virtual int WarningPeriod { get; set; }

		[Display(Name = "Smtp address")]
		public virtual string SmtpAddress { get; set; }

		[Display(Name = "Port number")]
		public virtual int PortNumber { get; set; }
		public virtual string UserName { get; set; }
		public virtual DateTime ModifiedDate { get; set; }
	}
}
