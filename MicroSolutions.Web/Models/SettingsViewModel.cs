using MicroSolutions.Data.Models;
using System.Collections.Generic;

namespace MicroSolutions.Web.Models
{
	public class SettingsViewModel
    {
        public virtual IEnumerable<ExpirationPeriod> ExpirationPeriod { get; set; }

        public virtual IEnumerable<ItemType> ItemType { get; set; }

        public virtual IEnumerable<Vendors> Vendors { get; set; }

		public virtual IEnumerable<Supplier> Supplier { get; set; }

		public virtual IEnumerable<EmailSettings> EmailSettings { get; set; }
	}
}