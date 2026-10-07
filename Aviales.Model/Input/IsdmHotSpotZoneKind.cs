using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Input
{

    [Table("IsdmHotSpotZoneKind", Schema = "Input")]
    [DisplayName("Вид зоны ИСДМ-точек")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class IsdmHotSpotZoneKind : AuditModelBase
    {
        [Key]
        public Guid IsdmHotSpotZoneKindId { get; set; }

        public int Code { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool IsActive { get; set; }
    }
}