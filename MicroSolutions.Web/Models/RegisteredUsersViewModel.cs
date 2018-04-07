using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSolutions.Web.Models
{
	public class RegisteredUsersViewModel
	{
		public virtual int UserId { get; set; }

		public virtual string UserName { get; set; }

		public virtual string Password { get; set; }

		public virtual DateTime CreatedDate { get; set; }

		public virtual DateTime? ModifiedDate { get; set; }

		public virtual string UserRole { get; set; }

		public virtual int? UserRoleId { get; set; }

		public virtual string IsActiveUser { get; set; }

		public virtual bool IsActiveUserState { get; set; }
	}
}