using System;

namespace MicroSolutions.Data.Models
{
	public class ExpirationPeriod
    {
        /// <summary>
        /// primary key value.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Expiration period name in months.
        /// </summary>
        public virtual string ExpirationPeriodName { get; set; }

        /// <summary>
        /// Expiration period value in months.
        /// </summary>
        public virtual int ExpirationPeriodValue { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }
	}
}
