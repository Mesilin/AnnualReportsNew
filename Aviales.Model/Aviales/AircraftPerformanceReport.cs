using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("AircraftPerformanceReport", Schema = "Aviales")]
    [DisplayName("Отчет по показателям работ воздушного судна")]
	[AssociativeToString("Отчет по показателям работ ВС от {AircraftPerformanceReportDate}")]
	[Serializable]
	public class AircraftPerformanceReport : AuditModelBase
	{
        [Key]
		public Guid AircraftPerformanceReportId {get; set;}

		public Guid RegionId {get; set;}
		public DateTime AircraftPerformanceReportDate {get; set;}
		public bool IsConfirmed {get; set;}

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;} = null!;

		public virtual ICollection<AircraftPerformance> AircraftPerformances { get; set; } = new List<AircraftPerformance>();

        public override string ToAuditString()
        {
            return "Отчет от "+ AircraftPerformanceReportDate.ToShortDateString();
        }

    }
}