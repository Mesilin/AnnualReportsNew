using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("GroundKind", Schema = "Catalog")]
	[DisplayName("’арактеристики местности и пожара.  атегори€ лесных и нелесных земель (архивный справочник)")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class GroundKind : AuditModelBase//, IKindType
	{
        [Key]
		public Guid GroundKindId {get; set;}

		public int Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}
		
        public override string ToAuditString()
        {
            return Name;
        }
}
}