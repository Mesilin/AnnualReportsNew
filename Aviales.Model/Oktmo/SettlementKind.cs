using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Oktmo
{

	[Table("SettlementKind", Schema = "Oktmo")]
	[DisplayName("Тип населённого пункта")]
	[AssociativeToString("{Name}")]
	[Serializable]
	public partial class SettlementKind : AuditModelBase
	{
		[Key]
		public Guid SettlementKindId {get; set;}

		[Required]
		[MaxLength(200)]
		public string? Name {get; set;}

		[Required]
		[MaxLength(50)]
		public string? ShortName {get; set;}

		public bool IsActive {get; set;}
		public virtual ICollection<SettlementOktmo> SettlementOktmoes {get; set; } = new List<SettlementOktmo>();
        public override string ToAuditString() => Name;
    }
}