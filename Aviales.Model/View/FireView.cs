using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aviales.Model.View
{
	[Table("FireView", Schema = "ForestFire")]
	public class FireView: FireViewParameters
	{
		/// <summary>
		/// Идентификатор пожара
		/// </summary>
		[Key]
		public Guid FireId { get; set; }
    }
}
