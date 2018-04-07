using System.ComponentModel.DataAnnotations;

namespace MicroSolutions.Web.Models
{
	public class ItemTypeViewModel
	{
		public virtual int Id { get; set; }

        [Display(Name = "Item type name")]
        [Required(ErrorMessage = "Item type name required.")]
		public virtual string ItemTypeName { get; set; }
	}
}