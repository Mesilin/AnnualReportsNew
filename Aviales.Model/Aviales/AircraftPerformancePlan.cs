using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Aviales
{

    [Table("AircraftPerformancePlan", Schema = "Aviales")]
    [DisplayName("Плановые показатели работы воздушных судов")]
    [Serializable]
    public partial class AircraftPerformancePlan : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid AircraftPerformancePlanId { get; set; }

        [DisplayName("Модель ВС")]
        public Guid AircraftModelId { get; set; }

        [DisplayName("Субъект")]
        public Guid RegionId { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Всего ВС")]
        public int AircraftCount { get; set; }

        [DisplayName("Налет на мониторинг")]
        public int? MonitoringFlightPlan { get; set; }

        [DisplayName("Налет на тушение")]
        public int? FireFightingFlightPlan { get; set; }

        [DisplayName("Год")]
        public int Year { get; set; }

        [ForeignKey("AircraftModelId")]
        public virtual AircraftModel AircraftModel { get; set; } = null!;

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; } = null!;

		[ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

}
}