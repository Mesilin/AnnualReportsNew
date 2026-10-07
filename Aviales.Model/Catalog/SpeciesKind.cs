using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Interfaces;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("SpeciesKind", Schema = "Catalog")]
	[DisplayName("Характеристики местности и пожара. Порода")]
	[Serializable]
    [AssociativeToString("{Name}")]
	public partial class SpeciesKind : AuditModelBase, ITaxationType
{
        [Key]
		public Guid SpeciesKindId {get; set;}

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