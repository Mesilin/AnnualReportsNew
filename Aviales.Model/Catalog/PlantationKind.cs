using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("PlantationKind", Schema = "Catalog")]
    [DisplayName("Характеристики горения. Характер")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class PlantationKind : AuditModelBase
	{
        [Key]
		public Guid PlantationKindId {get; set;}

		public int Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public string? ShortName2 {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}
		
        public override string ToAuditString()
        {
            return Name;
        }
    }
}