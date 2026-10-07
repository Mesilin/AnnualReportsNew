using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
    public abstract class FireDynamicParameters : AuditModelBase
    {
        [DisplayName("Дата и время динамики")]
        public DateTime FireDynamicDate { get; set; }

        [DisplayName("Состояние")]
        public Guid FireDynamicStateKindId { get; set; }

        [DisplayName("Вид")]
        public Guid? FireKindId { get; set; }

        [DisplayName("Характер")]
        public Guid? PlantationKindId { get; set; }

        [DisplayName("Интенсивность")]
        public Guid? FireIntensityKindId { get; set; }

        [DisplayName("Причина непринятия мер")]
        public Guid? NotLandingReasonKindId { get; set; }

        [DisplayName("Руководитель тушения пожара(Каталог)")]
        public Guid? FireManagerId { get; set; }

        [DisplayName("Покрытая площадь")]
        public decimal? CoverArea { get; set; }

        [DisplayName("Непокрытая площадь")]
        public decimal? NoncoverArea { get; set; }

        [DisplayName("Верховая площадь")]
        public decimal? CrownArea { get; set; }

        [DisplayName("Подземная площадь")]
        public decimal? UndergroundArea { get; set; }

        [DisplayName("Низовая площадь")]
        public decimal? GroundArea { get; set; }

        [DisplayName("Общая верховая площадь")]
        public decimal? TotalCrownArea { get; set; }

        [DisplayName("Общая низовая площадь")]
        public decimal? TotalGroundArea { get; set; }

        [DisplayName("Общая подземная площадь")]
        public decimal? TotalUndergroundArea { get; set; }

        [DisplayName("Прыжки")]
        public int? JumpCount { get; set; }

        [DisplayName("Спуски")]
        public int? DescentCount { get; set; }

        [DisplayName("Масса грузов, доставленных к месту пожара")]
        public decimal? CargoWeight { get; set; }

        [DisplayName("Масса грузов, доставленных к месту пожара авиационным транспортом")]
        public decimal? CargoAviaWeight { get; set; }

        [DisplayName("Нелесная площадь")]
        public decimal? NonforestArea { get; set; }

        [DisplayName("Покрытая площадь на арендуемых участках")]
        public decimal? LeasholderCoverArea { get; set; }

        [DisplayName("Верховая площадь на арендуемых участках")]
        public decimal? LeasholderCrownArea { get; set; }

        [DisplayName("Низовая площадь на арендуемых участках")]
        public decimal? LeasholderGroundArea { get; set; }

        [DisplayName("Подземная площадь на арендуемых участках")]
        public decimal? LeasholderUndergroundArea { get; set; }

        [DisplayName("Непокрытая площадь на арендуемых участках")]
        public decimal? LeasholderNoncoverArea { get; set; }

        [DisplayName("Нелесная площадь на арендуемых участках")]
        public decimal? LeasholderNonforestArea { get; set; }

        [DisplayName("Кромка")]
        public decimal? FireEdgeLength { get; set; }

        [DisplayName("Защитная площадь")]
        public decimal? ProtectionForestArea { get; set; }

        [DisplayName("Эксплуатационная площадь")]
        public decimal? ExploitableForestArea { get; set; }

        [DisplayName("Резервная площадь")]
        public decimal? ReservedForestArea { get; set; }

        [DisplayName("Общая защитная площадь")]
        public decimal? TotalProtectionForestArea { get; set; }

        [DisplayName("Общая эксплуатационная площадь")]
        public decimal? TotalExploitableForestArea { get; set; }

        [DisplayName("Общая резервная площадь")]
        public decimal? TotalReservedForestArea { get; set; }

        [DisplayName("Руководитель тушения пожара")]
        public string? FireFightManager { get; set; }

        [DisplayName("Примечание")]
        public string? Description { get; set; }

        [DisplayName("Угроза населенному пункту")]
        public bool IsSettlementDanger { get; set; }

        [MaxLength(150)]
        [DisplayName("Номер протокола КЧС")]
        public string? CesProtocolNumber { get; set; }

        [DisplayName("Дата протокола КЧС")]
        public DateTime? CesProtocolDate { get; set; }

        [ForeignKey("FireDynamicStateKindId")]
        public virtual FireDynamicStateKind? FireDynamicStateKind { get; set; }

        [ForeignKey("FireIntensityKindId")]
        public virtual FireIntensityKind? FireIntensityKind { get; set; }

        [ForeignKey("FireKindId")]
        public virtual FireKind? FireKind { get; set; }

        [ForeignKey("FireManagerId")]
        public virtual FireManager? FireManager { get; set; }

        [ForeignKey("NotLandingReasonKindId")]
        public virtual NotLandingReasonKind? NotLandingReasonKind { get; set; }

        [ForeignKey("PlantationKindId")]
        public virtual PlantationKind? PlantationKind { get; set; }
    }
}
