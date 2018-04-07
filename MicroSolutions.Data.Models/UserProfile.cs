using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSolutions.Data.Models
{
	public class UserProfile
	{
		[Key]
		public virtual int ID {get;set;}

		public virtual string UserName { get; set; }
	}
}
