using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("FireHazardKind", Schema = "Catalog")]
	[DisplayName("Вид пожароопасности")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class FireHazardKind : AuditModelBase
	{
		[Key]
		public Guid FireHazardKindId {get; set;}

		public int Code {get; set;}

		[Required]
		[MaxLength(256)]
		public string? Name {get; set;}

		[Required]
		[MaxLength(100)]
		public string? ShortName {get; set;}

		public int Year {get; set;}
		public virtual ICollection<FireHazard> FireHazards {get; set; } = new List<FireHazard>();

        public override string ToAuditString()
        {
            return Name;
        }
    }
}