using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("DetectionKind", Schema = "Catalog")]
    [DisplayName("Способы обнаружения")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class DetectionKind : AuditModelBase//, IKindType
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid DetectionKindId { get; set; }

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
        
        public override string ToAuditString()
        {
            return Name;
        }
    }
}