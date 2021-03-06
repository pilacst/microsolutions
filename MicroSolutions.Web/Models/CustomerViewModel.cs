﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class CustomerViewModel	{
		public virtual int Id { get; set; }

		[Display(Name = "Customer name")]
        [Required(ErrorMessage = "Customer name is required.")]
		public virtual string CustomerName { get; set; }

		[Display(Name = "Contact person name")]
        [Required(ErrorMessage = "Contact person name is required.")]
        public virtual string ContactPersonName { get; set; }

		[Display(Name = "Contact number")]
        [Required(ErrorMessage = "Contact number is required.")]
        public virtual string ContactNumber { get; set; }

		[Display(Name = "Customer address")]
		public virtual string Address { get; set; }

		[Display(Name = "Memo")]
		public virtual string Memorandum { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }
	}
}