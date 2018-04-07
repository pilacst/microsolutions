using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Web.Models
{
	public class ExpirationPeriodViewModel
    {
        [Required(ErrorMessage = "Expiration period is required")]
        public virtual int ExpirationPeriodId { get; set; }

        [StringLength(25)]
        public virtual string ExpirationPeriodName { get; set; }

		public virtual int ExpirationPeriodValue { get; set; }
	}
}