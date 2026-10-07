using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("FightFire2024ForceAvailabilityByFormationReport", Schema = "Aviales")]
    [DisplayName("Отчет по начилию сил и средств тушения по формированию")]
    [Serializable]
	[AssociativeToString("Отчет по начилию сил и средств от {FightFireForceAvailabilityReportDate}")]
	public partial class FightFire2024ForceAvailabilityByFormationReport : AuditModelBase
    {
        public FightFire2024ForceAvailabilityByFormationReport()
        {
            this.FightFireForceAvailabilitiesByFormations = new List<FightFire2024ForceAvailabilityByFormation>();
        }

        [Key]
        [DisplayName("Идентификатор отчета")]
        public Guid FightFireForceAvailabilityByFormationsReportId { get; set; }

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
        public virtual ICollection<FightFire2024ForceAvailabilityByFormation> FightFireForceAvailabilitiesByFormations { get; set; }

        public override string ToAuditString()
        {
            return "Отчет от " + FightFireForceAvailabilityReportDate.ToShortDateString();
        }
    }
}