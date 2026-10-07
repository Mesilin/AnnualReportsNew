using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="Incom.Common2.Persistence.AuditModelBase" />
	[Table("FightFireFormationResourceKind", Schema = "Catalog")]
	[DisplayName("Тип ресурсов лесопожарных формирований")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public class FightFireFormationResourceKind : AuditModelBase
	{
		[Key]
		public Guid FightFireFormationResourceKindId {get; set;}

		public int Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}
		public int Sequence {get; set;}
		public bool IsRegional {get; set;}
		public bool IsMunicipal {get; set;}
	}
}