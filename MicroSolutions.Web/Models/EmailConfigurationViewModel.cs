using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class EmailConfigurationViewModel
	{
		public virtual int Id { get; set; }

		[Display(Name = "From email")]
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
	}
}