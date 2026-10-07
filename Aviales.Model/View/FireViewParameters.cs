using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;

namespace Aviales.Model.View
{
	[Serializable]
	public class FireViewParameters : AuditModelBase, IProductionDependent
	{
		/// <summary>
		/// Идентификатор региона
		/// </summary>
		public Guid? RegionId { get; set; }

		/// <summary>
		/// Номер по лесничеству
		/// </summary>
		public int NumberForestry { get; set; }

		/// <summary>
		/// Номер по субъекту
		/// </summary>
		public int? NumberRegion { get; set; }

		/// <summary>
		/// Номер авиаотделения
		/// </summary>
		public int? NumberAirbaseDepartment { get; set; }

		/// <summary>
		/// Номер большого пожара
		/// </summary>
		public int? NumberBigFire { get; set; }

		/// <summary>
		/// Номер пожара по ИСДМ
		/// </summary>
		public int? NumberIsdm { get; set; }

		/// <summary>
		/// КПО обнаружения
		/// </summary>
		public byte? KpoDetection { get; set; }

		/// <summary>
		/// КПО ликвидации
		/// </summary>
		public byte? KpoLiquidation { get; set; }

		/// <summary>
		/// Дата начала тушения пожара
		/// </summary>
		public DateTime? FireFightBeginDate { get; set; }

		/// <summary>
		/// Первое сообщение о пожаре
		/// </summary>
		public DateTime? FirstMessageDate { get; set; }

		/// <summary>
		/// Широта
		/// </summary>
		public double? Latitude { get; set; }

		/// <summary>
		/// Долгота
		/// </summary>
		public double? Longitude { get; set; }

		/// <summary>
		/// Азимут от населенного пункта
		/// </summary>
		public decimal? AzimuthSettlement { get; set; }

		/// <summary>
		/// Азимут от авиаотделения
		/// </summary>
		public decimal? AzimuthAirbaseDepartment { get; set; }

		/// <summary>
		/// Удаление от населенного пункта
		/// </summary>
		public decimal? DistanceSettlement { get; set; }

		/// <summary>
		/// Удаление от авиаотделения
		/// </summary>
		public decimal? DistanceAirbaseDepartment { get; set; }

		/// <summary>
		/// Удаление от места высадки
		/// </summary>
		public decimal? DistanceLanding { get; set; }

		/// <summary>
		/// Удаление от транспортных путей
		/// </summary>
		public decimal? DistanceTrafficRoad { get; set; }

		/// <summary>
		/// Скорость ветра
		/// </summary>
		public decimal? WindStrength { get; set; }

		/// <summary>
		/// Дата фронтальных осадков
		/// </summary>
		public DateTime? FrontalPrecipitationDate { get; set; }

		/// <summary>
		/// Прямые затраты субъекта РФ
		/// </summary>
		public decimal? CostTotal { get; set; }

		/// <summary>
		/// Стоимость услуг BC
		/// </summary>
		public decimal? CostRentAircraft { get; set; }

		/// <summary>
		/// Общий ущерб
		/// </summary>
		public decimal? CostDamage { get; set; }

		/// <summary>
		/// Дата акта (протокола)
		/// </summary>
		public DateTime? ProtocolDate { get; set; }

		/// <summary>
		/// Номер акта (протокола)
		/// </summary>
		public string? ProtocolNumber { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Результат проверки пожара
		/// </summary>
		public string? ValidationResult { get; set; }

		/// <summary>
		/// Идентификатор авиаотделения
		/// </summary>
		public Guid? AirbaseDepartmentId { get; set; }

		/// <summary>
		/// Идентификатор лесничества
		/// </summary>
		public Guid ForestryId { get; set; }

		/// <summary>
		/// Идентификатор участкового лесничества
		/// </summary>
		public Guid? ForestryDistrictId { get; set; }

		/// <summary>
		/// Идентификатор административного района
		/// </summary>
		public Guid? MunicipalDistrictId { get; set; }

		/// <summary>
		/// Идентификатор ближайшего населенного пункта
		/// </summary>
		public Guid? SettlementId { get; set; }

		/// <summary>
		/// Идентификатор района применения сил и средств
		/// </summary>
		public Guid? FightFireZoneId { get; set; }

		/// <summary>
		/// Идентификатор породы
		/// </summary>
		public Guid? SpeciesKindId { get; set; }

		/// <summary>
		/// Идентификатор покрова
		/// </summary>
		public Guid? CoverKindId { get; set; }

		/// <summary>
		/// Идентификатор "Целевое назначение лесов"
		/// </summary>
		public Guid? ForestKindId { get; set; }

		/// <summary>
		/// Идентификатор категории земель
		/// </summary>
		public Guid? GroundKindId { get; set; }

		/// <summary>
		/// Идентификатор причины пожаров
		/// </summary>
		public Guid? BeginFireReasonKindId { get; set; }

		/// <summary>
		/// Идентификатор способа обнаружения
		/// </summary>
		public Guid? DetectionKindId { get; set; }

		/// <summary>
		/// Идентификаторы арендаторов
		/// </summary>
		public string? LeaseholderIds { get; set; }

		/// <summary>
		/// Идентификатор принадлежности
		/// </summary>
		public Guid ForestOwnerKindId { get; set; }

		/// <summary>
		/// Идентификатор зоны
		/// </summary>
		public Guid MonitoringZoneKindId { get; set; }

		/// <summary>
		/// Наименование авиаотеделения
		/// </summary>
		public string? AirbaseDepartmentName { get; set; }

		/// <summary>
		/// Регион
		/// </summary>
		public string? RegionName { get; set; }

		/// <summary>
		/// Участковое лесничество
		/// </summary>
		public string? ForestryDistrictName { get; set; }

		/// <summary>
		/// Административный район
		/// </summary>
		public string? MunicipalDistrictName { get; set; }

		/// <summary>
		/// Ближайший населенный пункт
		/// </summary>
		public string? SettlementName { get; set; }

		/// <summary>
		/// Порода
		/// </summary>
		public string? SpeciesKindName { get; set; }

		/// <summary>
		/// Покров
		/// </summary>
		public string? CoverKindName { get; set; }

		/// <summary>
		/// Целевое назначение лесов
		/// </summary>
		public string? ForestKindName { get; set; }

		/// <summary>
		/// Категория земель
		/// </summary>
		public string? GroundKindName { get; set; }

		/// <summary>
		/// Причины пожара
		/// </summary>
		public string? BeginFireReasonKindName { get; set; }

		/// <summary>
		/// Cпособ обнаружения
		/// </summary>
		public string? DetectionKindName { get; set; }

		/// <summary>
		/// Район применения сил и средств
		/// </summary>
		public string? FightFireZoneName { get; set; }

		/// <summary>
		/// Арендатор
		/// </summary>
		public string? LeaseholderNames { get; set; }

		/// <summary>
		/// Зона мониторинга
		/// </summary>
		public string? MonitoringZoneKindName { get; set; }

		/// <summary>
		/// Принадлежность леса
		/// </summary>
		public string? ForestOwnerKindName { get; set; }

		public int Year { get; set; }

		public string? StateName { get; set; }

		/// <summary>
		/// Лесничество
		/// </summary>
		public string? ForestryName { get; set; }

		public Guid? LastFireDynamicId { get; set; }
		public DateTime? LastFireDynamicDate { get; set; }
		public decimal? LastFireDynamicCoverArea { get; set; }
		public decimal? LastFireDynamicNoncoverArea { get; set; }
		public decimal? LastFireDynamicCrownArea { get; set; }
		public decimal? LastFireDynamicUndergroundArea { get; set; }
		public decimal? LastFireDynamicGroundArea { get; set; }

		public decimal? TotalCrownArea { get; set; }
		public decimal? TotalUndergroundArea { get; set; }
		public decimal? TotalGroundArea { get; set; }

		public decimal? LastFireDynamicNonforestArea { get; set; }
		public decimal? LastFireDynamicStateKindCode { get; set; }
		public string? LastFireDynamicFireFightManager { get; set; }
		public decimal? TotalArea { get; set; }
		public decimal? ForestArea { get; set; }

		public Guid? FirstFireDynamicId { get; set; }
		public DateTime? FirstFireDynamicDate { get; set; }
		public decimal? FirstFireDynamicCoverArea { get; set; }
		public decimal? FirstFireDynamicNoncoverArea { get; set; }
		public decimal? FirstFireDynamicCrownArea { get; set; }
		public decimal? FirstFireDynamicUndergroundArea { get; set; }
		public decimal? FirstFireDynamicGroundArea { get; set; }
		public decimal? FirstFireDynamicNonforestArea { get; set; }
		public decimal? TotalDetectingArea { get; set; }

		public decimal? FirstFireDynamicTotalCrownArea { get; set; }
		public decimal? FirstFireDynamicTotalUndergroundArea { get; set; }
		public decimal? FirstFireDynamicTotalGroundArea { get; set; }

		/////// <summary>
		/////// Урочище
		/////// </summary>
		////public string? ForestryTract { get; set; }

		/////// <summary>
		/////// Квартал
		/////// </summary>
		////public string? Quarter { get; set; }

		/////// <summary>
		/////// Выдел
		/////// </summary>
		////public string? Stratum { get; set; }

		/// <summary>
		/// Признак наличия у пожара динамик с заполнеными полями КЧС (CesProtocolDate, CesProtocolNumber)
		/// </summary>
		public bool? IsCes { get; set; }
		public Guid? ProductionId { get; set; }

		[DisplayName("Дата когда пожар стал крупным")]
		public DateTime? BigFireDate { get; set; }

		[NotMapped]
		public bool IsBigFire { get; set; }

		[NotMapped]
		public bool HasDynamics
		{
			get
			{
				bool hasDynamics = FirstFireDynamicDate.HasValue || LastFireDynamicDate.HasValue;
				return hasDynamics;
			}

		}

		[NotMapped]
		public int FireFlightWorksCount { get; set; }
	}
}
