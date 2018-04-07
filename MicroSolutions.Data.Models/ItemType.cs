using System;
using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Data.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ItemType
    {
        /// <summary>
        /// Item type id.
        /// </summary>
        public virtual int Id { get; set; }

		/// <summary>
		/// Item type name.
		/// </summary>
		[Display(Name = "Item type")]
        public virtual string ItemTypeName { get; set; }

		public virtual bool Status { get; set; }

		public virtual string UserName { get; set; }

		public virtual DateTime ModifiedDate { get; set; }
	}
}
