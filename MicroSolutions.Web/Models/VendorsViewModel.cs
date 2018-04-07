using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class VendorsViewModel
	{
		public virtual int Id { get; set; }

        [Display(Name = "Vendor name")]
        [Required(ErrorMessage = "Vendor name is required.")]
		public virtual string VenderName { get; set; }
	}
}