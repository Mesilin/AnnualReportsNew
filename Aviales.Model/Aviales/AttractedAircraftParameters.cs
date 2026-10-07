using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.Aviales
{
    [Serializable]
    public class AttractedAircraftParameters : AuditModelBase
    {
        [Required]
        [MaxLength(100)]
        [DisplayName("Марка ВС")]
        public string? Mark { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Номер")]
        public string? Number { get; set; }

        [DisplayName("Сливы")]
        public int? PourCount { get; set; }

        [DisplayName("Объём Сливов")]
        public decimal? PourVolume { get; set; }

        [DisplayName("Идентификатор типа подразделения")]
        public Guid? FightFireTeamLocalId { get; set; }

        [ForeignKey("FightFireTeamLocalId")]
        [DisplayName("Тип команды")]
        public virtual FightFireTeamLocal? FightFireTeamLocal { get; set; }
    }
}
