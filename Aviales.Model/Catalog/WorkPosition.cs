using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
    [Table("WorkPosition", Schema = "Catalog")]
	[DisplayName("Должность")]
	[Serializable]
    [AssociativeToString("{Name}")]
	public partial class WorkPosition : AuditModelBase
	{
        [Key]
		public System.Guid WorkPositionId {get; set;}

		[Required]
		public string? Name {get; set;}

		public int Code {get; set;}
		public bool IsActive {get; set;}
		
		public virtual ICollection<WorkPosition2WorkPositionGroup> WorkPosition2WorkPositionGroup {get; set;} = new List<WorkPosition2WorkPositionGroup>();
    }
}