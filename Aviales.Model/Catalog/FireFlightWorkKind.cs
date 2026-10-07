using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
    /// <summary>
    /// Вид работ для налёта
    /// </summary>
    [Table("FireFlightWorkKind", Schema = "Catalog")]
    [DisplayName("Вид работ для налёта")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FireFlightWorkKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireFlightWorkKindId { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Код")]
        public string? Code { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}