using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Data.Models
{
	public class webpages_Roles
	{
		[Key]
		public virtual int RoleId { get; set; }

		public virtual string RoleName { get; set; }
	}
}
