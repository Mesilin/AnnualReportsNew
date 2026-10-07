using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("FightFireForceAvailabilityByFormationReport", Schema = "Aviales")]
    [DisplayName("Отчет по начилию сил и средств тушения по формированию")]
    [Serializable]
	[AssociativeToString("Отчет по начилию сил и средств от {FightFireForceAvailabilityReportDate}")]
	public partial class FightFireForceAvailabilityByFormationReport : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор отчета")]
        public Guid FightFireForceAvailabilityByFormationReportId { get; set; }

        [DisplayName("Идентификатор субъекта")]
        public Guid RegionId { get; set; }

        [DisplayName("Дата отчета")]
        public DateTime FightFireForceAvailabilityReportDate { get; set; }

        [DisplayName("Отчет подтвержден?")]
        public bool IsConfirmed { get; set; }

        [ForeignKey("RegionId")]
        [DisplayName("Субъект")]
        public virtual Region Region { get; set; }

        [Aggregation]
        [DisplayName("Список показателей по формированиям")]
        public virtual ICollection<FightFireForceAvailabilityByFormation> FightFireForceAvailabilitiesByFormations { get; set; } = new List<FightFireForceAvailabilityByFormation>();

        public override string ToAuditString()
        {
            return "Отчет от " + FightFireForceAvailabilityReportDate.ToShortDateString();
        }
    }
}