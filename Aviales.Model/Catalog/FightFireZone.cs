using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FightFireZone", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Районы применения сил и средств пожаротушения")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireZone : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FightFireZoneId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(3)]
        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год справочника")]
        public int? Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }

}