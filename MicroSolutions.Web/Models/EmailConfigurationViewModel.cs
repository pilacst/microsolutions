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
        [Required(ErrorMessage = "From email field is required.")]
		public virtual string FromEmail { get; set; }

		[Display(Name = "From email password")]
        [Required(ErrorMessage = "From email password field is required.")]
        public virtual string FromEmailPassword { get; set; }

		[Display(Name = "To email")]
        [Required(ErrorMessage = "To email field is required.")]
        public virtual string ToEmail { get; set; }

		[Display(Name = "Warning period")]
        [Required(ErrorMessage = "Warning period field is required.")]
        public virtual int WarningPeriod { get; set; }

		[Display(Name = "Smtp address")]
        [Required(ErrorMessage = "Smtp address field is required.")]
        public virtual string SmtpAddress { get; set; }

		[Display(Name = "Port number")]
        [Required(ErrorMessage = "Port number field is required.")]
        public virtual int PortNumber { get; set; }
	}
}