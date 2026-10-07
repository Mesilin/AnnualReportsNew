using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FireIntensityKind", Schema = "Catalog")]
    [DisplayName("Характеристики горения. Интенсивность")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FireIntensityKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireIntensityKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }
        
        [DisplayName("Список динамик")]
        public virtual ICollection<FireDynamic> FireDynamics { get; set; } = new List<FireDynamic>();

        public override string ToAuditString()
        {
            return Name;
        }
    }
}