using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FireKind", Schema = "Catalog")]
    [DisplayName("Характеристики горения. Вид")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FireKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год")]
        public int? Year { get; set; }

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