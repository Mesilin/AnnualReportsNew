using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("Consequence", Schema = "Aviales")]
    [DisplayName("Потери")]
	[AssociativeToString("Потери от {Date}")]
	public partial class Consequence : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid ConsequenceId { get; set; }

        [DisplayName("Идентификатор субъекта")]
        public Guid RegionId { get; set; }

        [Required]
        [Column("ConsequenceDate")]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Количество погибших")]
        public int? DeadCount { get; set; }

        [DisplayName("Количество пострадавших")]
        public int? VictimCount { get; set; }

        [DisplayName("Пострадало объектов")]
        public int? DestructionCount { get; set; }

        [DisplayName("Муниципальный район")]
        public Guid? MunicipalDistrictId { get; set; }

        [MaxLength(2000)]
        [DisplayName("Примечание")]
        public string? Note { get; set; }

        [ForeignKey("RegionId")]
        [DisplayName("Субъект")]
        public virtual Region Region { get; set; }

        [ForeignKey("MunicipalDistrictId")]
        public virtual SettlementOktmo? MunicipalDistrict { get; set; }

        public override string ToAuditString()
        {
            return "Потери от " + Date.ToShortDateString();
        }
    }
}