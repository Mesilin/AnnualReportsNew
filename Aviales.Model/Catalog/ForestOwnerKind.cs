using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("ForestOwnerKind", Schema = "Catalog")]
    [DisplayName("Принадлежность земель")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class ForestOwnerKind : AuditModelBase//, IKindType
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid ForestOwnerKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Сокращенное наименование")]
        public string? ShortName2 { get; set; }

        [DisplayName("Год справочника")]
        public int? Year { get; set; }

        [DisplayName("Порядок сортировки")]
        public int? Sorting { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        public override string ToAuditString()
        {
            return ShortName;
        }
    }
}