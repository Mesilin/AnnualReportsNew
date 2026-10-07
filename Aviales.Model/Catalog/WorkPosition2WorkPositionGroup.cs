using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Catalog
{
	[Table("WorkPosition2WorkPositionGroup", Schema = "Catalog")]
	[DisplayName("Связка должности и группы должностей")]
	[Serializable]
	public partial class WorkPosition2WorkPositionGroup : ModelBase
	{

		[Key]
		public System.Guid WorkPosition2WorkPositionGroupId { get; set; }
		public System.Guid WorkPositionId { get; set; }
		public System.Guid WorkPositionGroupId { get; set; }
		[ForeignKey("WorkPositionId")]
		public virtual WorkPosition WorkPosition { get; set; }
		[ForeignKey("WorkPositionGroupId")]
		public virtual WorkPositionGroup WorkPositionGroup { get; set; }
	}
}