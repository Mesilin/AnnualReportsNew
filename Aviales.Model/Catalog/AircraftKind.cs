using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("AircraftKind", Schema = "Catalog")]
    [DisplayName("Тип воздушного судна")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class AircraftKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid AircraftKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Список моделей")]
        public virtual ICollection<AircraftModel> AircraftModels { get; set; } = null!;
        public override string ToAuditString()
        {
            return Name;
        }
    }
}