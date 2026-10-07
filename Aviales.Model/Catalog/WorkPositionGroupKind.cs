using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("WorkPositionGroupKind", Schema = "Catalog")]
	[DisplayName("Вид групп должностей")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class WorkPositionGroupKind : AuditModelBase
	{
        [Key]
		public Guid WorkPositionGroupKindId {get; set;}

		[Required]
		public string? Name {get; set;}

		public virtual ICollection<WorkPositionGroup> WorkPositionGroups {get; set; } = new List<WorkPositionGroup>();
    }
}