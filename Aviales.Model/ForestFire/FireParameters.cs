using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Oktmo;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.ForestFire
{
	[Serializable]
	public abstract class FireParameters : AuditModelBase, IProductionDependent
	{
		[DisplayName("Принадлежность лесов")]
		public Guid ForestOwnerKindId { get; set; }

		[DisplayName("Зона мониторинга")]
		public Guid MonitoringZoneKindId { get; set; }

		[DisplayName("Район применения сил и средств")]
		public Guid FightFireZoneId { get; set; }

		[DisplayName("Лесничество")]
		public Guid ForestryId { get; set; }

		[DisplayName("Авиаотделение")]
		public Guid? AirbaseDepartmentId { get; set; }

		[DisplayName("Целевое назначение лесов")]
		public Guid? ForestKindId { get; set; }

		[DisplayName("Категория лесных и нелесных земель")]
		public Guid? GroundKindId { get; set; }
		
		[DisplayName("Порода")]
		public Guid? SpeciesKindId { get; set; }

		[DisplayName("Причина пожара")]
		public Guid? BeginFireReasonKindId { get; set; }

		[DisplayName("Покров")]
		public Guid? CoverKindId { get; set; }

		[DisplayName("Способ обнаружения")]
		public Guid? DetectionKindId { get; set; }

		[DisplayName("Участковое лесничество")]
		public Guid? ForestryDistrictId { get; set; }

		[IsBasic]
		[DisplayName("Номер пожара по авиаотделению")]
		public int? NumberAirbaseDepartment { get; set; }

		[IsBasic]
		[DisplayName("Номер крупного пожара")]
		public int? NumberBigFire { get; set; }

		[IsBasic]
		[DisplayName("Номер ИСДМ")]
		public int? NumberIsdm { get; set; }

		[DisplayName("Азимут от авиаотделения до пожара")]
		public decimal? AzimuthAirbaseDepartment { get; set; }

		[DisplayName("Дальность от авиаотделения до пожара")]
		public decimal? DistanceAirbaseDepartment { get; set; }

		[DisplayName("Азимут от/до населенного пункта")]
		public decimal? AzimuthSettlement { get; set; }

		[DisplayName("Дальность до населенного пункта")]
		public decimal? DistanceSettlement { get; set; }

		[DisplayName("Расстояние до места высадки")]
		public decimal? DistanceLanding { get; set; }

		[DisplayName("Расстояние до транспортных путей")]
		public decimal? DistanceTrafficRoad { get; set; }

		[DisplayName("Дата фронтальных осадков")]
		public DateTime? FrontalPrecipitationDate { get; set; }

		[MaxLength(150)]
		[DisplayName("Номер акта (протокола)")]
		public string? ProtocolNumber { get; set; }

		[DisplayName("Дата акта (протокола)")]
		public DateTime? ProtocolDate { get; set; }

		[DisplayName("Начало тушения пожара")]
		public DateTime? FireFightBeginDate { get; set; }

		[DisplayName("Примечание")]
		public string? Description { get; set; }

		[DisplayName("Результат проверки пожара")]
		public string? ValidationResult { get; set; }

		[DisplayName("Первое сообщение о пожаре")]
		public DateTime? FirstMessageDate { get; set; }

		[DisplayName("Скорость ветра")]
		public decimal? WindStrength { get; set; }

		[DisplayName("КПО в день обнаружения")]
		public byte? KpoDetection { get; set; }

		[DisplayName("КПО в день ликвидации")]
		public byte? KpoLiquidation { get; set; }

		[IsBasic]
		[DisplayName("Номер пожара по лесничеству")]
		public int NumberForestry { get; set; }

		[IsBasic]
		[DisplayName("Номер пожара по субъекту")]
		public int? NumberRegion { get; set; }

		[DisplayName("Широта")]
		public double? Latitude { get; set; }

		[DisplayName("Долгота")]
		public double? Longitude { get; set; }

		[DisplayName("Прямые затраты субъекта РФ на тушение из всех источников")]
		public decimal? CostTotal { get; set; }

		[DisplayName("Стоимость услуг по найму ВС")]
		public decimal? CostRentAircraft { get; set; }

		[DisplayName("Общий ущерб")]
		public decimal? CostDamage { get; set; }

		[DisplayName("Год")]
		public int Year { get; set; }

		[DisplayName("Административный район")]
		public Guid? MunicipalDistrictId { get; set; }

		[DisplayName("Ближайший населенный пункт")]
		public Guid? SettlementId { get; set; }

		[DisplayName("Наименование лесничества")]
		public string? ForestryName { get; set; }

		[DisplayName("Дата когда пожар стал крупным")]
		public DateTime? BigFireDate { get; set; }

		[DisplayName("Идентификатор внедрения")]
		public Guid? ProductionId { get; set; }

		[ForeignKey("AirbaseDepartmentId")]
		[DisplayName("Авиаотделение")]
		public virtual AirbaseDepartment? AirbaseDepartment { get; set; }

		[ForeignKey("ForestryId")]
		[DisplayName("Лесничество")]
		public virtual Forestry? Forestry { get; set; }

		[ForeignKey("ForestryDistrictId")]
		[DisplayName("Уч. лесничество")]
		public virtual ForestryDistrict? ForestryDistrict { get; set; }

		[ForeignKey("BeginFireReasonKindId")]
		[DisplayName("Причина пожара")]
		public virtual BeginFireReasonKind? BeginFireReasonKind { get; set; }

		[ForeignKey("CoverKindId")]
		[DisplayName("Вид покрытия")]
		public virtual CoverKind? CoverKind { get; set; }

		[ForeignKey("FightFireZoneId")]
		[DisplayName("Район применения сил и средств")]
		public virtual FightFireZone? FightFireZone { get; set; }

		[ForeignKey("DetectionKindId")]
		[DisplayName("Способ обнаружения")]
		public virtual DetectionKind? DetectionKind { get; set; }

		[ForeignKey("ForestKindId")]
		[DisplayName("Назначение лесов")]
		public virtual ForestKind? ForestKind { get; set; }

		[ForeignKey("ForestOwnerKindId")]
		[DisplayName("Принадлежность земель")]
		public virtual ForestOwnerKind? ForestOwnerKind { get; set; }

		[ForeignKey("GroundKindId")]
		[DisplayName("Категория земель")]
		public virtual GroundKind? GroundKind { get; set; }

		[ForeignKey("MonitoringZoneKindId")]
		[DisplayName("Зона мониторинга")]
		public virtual MonitoringZoneKind? MonitoringZoneKind { get; set; }

		[ForeignKey("SpeciesKindId")]
		[DisplayName("Порода")]
		public virtual SpeciesKind? SpeciesKind { get; set; }

		[ForeignKey("MunicipalDistrictId")]
		[DisplayName("Мун. р-н")]
		public virtual SettlementOktmo? MunicipalDistrict { get; set; }

		[ForeignKey("SettlementId")]
		[DisplayName("Ближайший н.п.")]
		public virtual SettlementOktmo? Settlement { get; set; }
		
		[ForeignKey("ProductionId")]
		[DisplayName("Внедрение")]
		public virtual Production? Production { get; set; }
	}
}
