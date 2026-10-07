using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

    [Table("AircraftPerformance", Schema = "Aviales")]
    [DisplayName("Показатели работы воздушного судна")]
    [Serializable]
    [AssociativeToString("План работы ВС: {AircraftModel}")]
public partial class AircraftPerformance : AuditModelBase
    {
        [Key] [DisplayName("Идентификатор")] public Guid AircraftPerformanceId { get; set; }

        [DisplayName("Идентификатор отчета")] public Guid AircraftPerformanceReportId { get; set; }

        [DisplayName("Модель ВС")] public Guid AircraftModelId { get; set; }

        [DisplayName("Категория земель(Принадлежность)")]
        public Guid? ForestOwnerKindId { get; set; }

        [DisplayName("Вылетов на мониторинг")] public int? MonitoringCount { get; set; }

        [DisplayName("Налет на мониторинг")] public int? MonitoringFlight { get; set; }

        [DisplayName("Вылетов на тушение")] public int? FireFightingCount { get; set; }

        [DisplayName("Налет на тушение")] public int? FireFightingFlight { get; set; }

        [DisplayName("Количество готовых ВС")] public int? ReadyCount { get; set; }

        [ForeignKey("AircraftModelId")] public virtual AircraftModel AircraftModel { get; set; } = null!;

		[ForeignKey("AircraftPerformanceReportId")]
        public virtual AircraftPerformanceReport? AircraftPerformanceReport { get; set; }

        [ForeignKey("ForestOwnerKindId")]
        [DisplayName("Категория земель(Принадлежность)")]
        public virtual ForestOwnerKind? ForestOwnerKind { get; set; }

        public override string ToAuditString()
        {
            return "Показатели работы ВС";
        }
    }
}