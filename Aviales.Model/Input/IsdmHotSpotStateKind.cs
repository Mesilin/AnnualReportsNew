using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Input
{

    [Table("IsdmHotSpotStateKind", Schema = "Input")]
    [DisplayName("Вид статуса ИСДМ-точек")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class IsdmHotSpotStateKind : AuditModelBase
    {
        [Key]
        public System.Guid IsdmHotSpotStateKindId { get; set; }

        public int Code { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool IsActive { get; set; }
    }
}