using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSolutions.Data.Models
{
	public class webpages_UsersInRoles
	{
		[Key]
		[ForeignKey("UserProfile")]
		[Column(Order =1)]
		public virtual int UserId { get; set; }

		[Key]
		[ForeignKey("webpages_Roles")]
		[Column(Order = 2)]
		public virtual int RoleId { get; set; }

		public virtual UserProfile UserProfile { get; set; }

		public virtual webpages_Roles webpages_Roles { get; set; }
	}
}
