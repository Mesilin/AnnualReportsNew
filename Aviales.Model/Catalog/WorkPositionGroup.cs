using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("WorkPositionGroup", Schema = "Catalog")]
	[DisplayName("Группа должностей")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class WorkPositionGroup : AuditModelBase
	{
        [Key]
		public Guid WorkPositionGroupId {get; set;}

		public Guid WorkPositionGroupKindId {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public int Code {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}
		
		[ForeignKey("WorkPositionGroupKindId")]
		public virtual WorkPositionGroupKind WorkPositionGroupKind {get; set;}

		public virtual ICollection<WorkPosition2WorkPositionGroup> WorkPosition2WorkPositionGroup {get; set;} = new List<WorkPosition2WorkPositionGroup>();
    }
}