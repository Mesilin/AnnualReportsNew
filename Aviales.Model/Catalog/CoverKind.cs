using Aviales.Model.Interfaces;
using Incom.Common2.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("CoverKind", Schema = "Catalog")]
    [DisplayName("Вид покрытия")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class CoverKind : AuditModelBase, ITaxationType
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid CoverKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Год")]
        public int? Year { get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }
        
        public override string ToAuditString()
        {
            return Name;
        }
    }
}