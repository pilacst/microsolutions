using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSolutions.Data.Models
{
	public class webpages_Membership
	{
		[Required]
		[Key]
		[ForeignKey("UserProfile")]
		public int UserId { get; set; }
		public DateTime CreateDate { get; set; }
		public string ConfirmationToken { get; set; }
		public bool IsConfirmed { get; set; }
		public DateTime? LastPasswordFailureDate { get; set; }
		[Required]
		public int PasswordFailuresSinceLastSuccess { get; set; }
		public string Password { get; set; }
		public DateTime? PasswordChangedDate { get; set; }
		public string PasswordSalt { get; set; }
		public string PasswordVerificationToken { get; set; }
		public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

		public virtual UserProfile UserProfile { get; set; }
	}
}
