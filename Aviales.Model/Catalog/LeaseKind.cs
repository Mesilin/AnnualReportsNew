using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("LeaseKind", Schema = "Catalog")]
	[DisplayName("Федеральные справочники. Виды использования лесов")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class LeaseKind : AuditModelBase
	{
        [Key]
		public Guid LeaseKindId {get; set;}

		[Required]
		public string? Name {get; set;}

		public bool IsActive {get; set;}
		
	}
}