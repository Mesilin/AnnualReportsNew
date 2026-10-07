using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FightFireResourceKind", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Типы средств тушения")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireResourceKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FightFireResourceKindId { get; set; }

        [DisplayName("Идентификатор")]
        public Guid? FightFireForceKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год справочника")]
        public int? Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [ForeignKey("FightFireForceKindId")]
        [DisplayName("Тип сил и средств пожаротушения")]
        public virtual FightFireForceKind? FightFireForceKind { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}