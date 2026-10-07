using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("FightFireTeamKind", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Типы команд")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireTeamKind : AuditModelBase
	{
        [Key]
		public Guid FightFireTeamKindId {get; set;}

		public Guid? FightFireForceKindId { get; set; }

		public int Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}

		[ForeignKey("FightFireForceKindId")]
		public virtual FightFireForceKind? FightFireForceKind { get; set; }
		
	}
}