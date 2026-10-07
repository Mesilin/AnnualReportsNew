/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFire.cs
* Автор: Паньков М.А.
* Дата создания: 25.10.2021
* Назначение: Определение класса LandscapeFire
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Oktmo;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFire", Schema = "LandscapeFire")]
    [DisplayName("Ландшафтный пожар")]
    [Serializable]
	[AssociativeToString("Ландшафтный пожар \u2116 {Number}")]
	public class LandscapeFire : AuditModelBase
    {
        /// <summary>
        /// Номер ландшафтного пожара 
        /// </summary>
        private int number;

        /// <summary>
        /// Первое сообщение о пожаре
        /// </summary>
        private DateTime? firstMessageDate;

        /// <summary>
        /// Начало тушения пожара
        /// </summary>
        private DateTime? beginExtinguishingDate;

        /// <summary>
        /// Широта
        /// </summary>
        private double? latitude;

        /// <summary>
        /// Долгота
        /// </summary>
        private double? longitude;

        /// <summary>
        /// Муниципальный район 
        /// </summary>
        private Guid municipalDistrictId;

        /// <summary>
        /// Ближайший населенный пункт 
        /// </summary>
        private Guid? settlementId;

        /// <summary>
        /// Азимут от/до населенного пункта
        /// </summary>
        private decimal? azimuthSettlement;

        /// <summary>
        /// Дальность от/до населенного пункта, км
        /// </summary>
        private decimal? distanceSettlement;
        /// <summary>
        /// Причина пожара
        /// </summary>
        private Guid? beginReasonKindId;

        /// <summary>
        /// Способ обнаружения
        /// </summary>
        private Guid? detectionKindId;

        /// <summary>
        /// Стоимость работ по тушению
        /// </summary>
        private decimal? costTotal;

        /// <summary>
        /// Общий ущерб
        /// </summary>
        private decimal? costDamage;

        /// <summary>
        /// Примечание
        /// </summary>
        private string? description;

        /// <summary>
        /// Азимут от авиаотделения до пожара
        /// </summary>
        private decimal? azimuthAirbaseDepartment;

        /// <summary>
        /// Дальность от авиаотделения до пожара
        /// </summary>
        private decimal? distanceAirbaseDepartment;

        /// <summary>
        /// Скорость ветра
        /// </summary>
        private decimal? windStrength;

        /// <summary>
        /// КПО в день обнаружения
        /// </summary>
        private byte? kpoDetection;

        /// <summary>
        /// КПО в день ликвидации
        /// </summary>
        private byte? kpoLiquidation;

        /// <summary>
        /// Принадлежность лесов
        /// </summary>
        private Guid forestOwnerKindId;

        /// <summary>
        /// Зона мониторинга
        /// </summary>
        private Guid monitoringZoneKindId;

        /// <summary>
        /// Район применения сил и средств
        /// </summary>
        private Guid fightFireZoneId;

        /// <summary>
        /// Авиаотделение
        /// </summary>
        private Guid? airbaseDepartmentId;

        /// <summary>
        /// Покров
        /// </summary>
        private Guid? coverKindId;

        /// <summary>
        /// Номер пожара по авиаотделению
        /// </summary>
        private int? numberAirbaseDepartment;

        /// <summary>
        /// Номер пожара административному району
        /// </summary>
        private int? numberMunicipalDistrict;

        /// <summary>
        /// Номер крупного пожара
        /// </summary>
        private int? numberBigFire;

        /// <summary>
        /// Номер ИСДМ
        /// </summary>
        private int? numberIsdm;

        /// <summary>
        /// Расстояние до места высадки
        /// </summary>
        private decimal? distanceLanding;

        /// <summary>
        /// Расстояние до транспортных путей
        /// </summary>
        private decimal? distanceTrafficRoad;

        /// <summary>
        /// Дата фронтальных осадков
        /// </summary>
        private DateTime? frontalPrecipitationDate;

        ///// <summary>
        ///// Номер пожара по субъекту
        ///// </summary>
        //private int numberRegion;

        /// <summary>
        /// Стоимость услуг по найму ВС
        /// </summary>
        private decimal? costRentAircraft;

        /// <summary>
        /// Акт о ландшафтном пожаре
        /// </summary>
        private Guid? landscapeFireActId;
        
        [Key]
        [DisplayName("Идентификатор ландшафтного пожара")]
        public Guid LandscapeFireId { get; set; }

        [DisplayName("Идентификатор региона")]
        public Guid? RegionId { get; set; }

        [DisplayName("Номер ландшафтного пожара")]
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Первое сообщение о пожаре")]
        public DateTime? FirstMessageDate
        {
            get => firstMessageDate;
            set
            {
                firstMessageDate = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Начало тушения пожара")]
        public DateTime? BeginExtinguishingDate
        {
            get => beginExtinguishingDate;
            set
            {
                beginExtinguishingDate = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Широта")]
        public double? Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Долгота")]
        public double? Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Муниципальный район")]
        public Guid MunicipalDistrictId
        {
            get => municipalDistrictId;
            set
            {
                municipalDistrictId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Ближайший населенный пункт ")]
        public Guid? SettlementId
        {
            get => settlementId;
            set
            {
                settlementId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Азимут от/до населенного пункта")]
        public decimal? AzimuthSettlement
        {
            get => azimuthSettlement;
            set
            {
                azimuthSettlement = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Дальность от/до населенного пункта, км")]
        public decimal? DistanceSettlement
        {
            get => distanceSettlement;
            set
            {
                distanceSettlement = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Причина пожара")]
        public Guid? BeginReasonKindId
        {
            get => beginReasonKindId;
            set
            {
                beginReasonKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Способ обнаружения")]
        public Guid? DetectionKindId
        {
            get => detectionKindId;
            set
            {
                detectionKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Стоимость работ по тушению")]
        public decimal? CostTotal
        {
            get => costTotal;
            set
            {
                costTotal = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Общий ущерб")]
        public decimal? CostDamage
        {
            get => costDamage;
            set
            {
                costDamage = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Примечание")]
        public string? Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Принадлежность лесов")]
        public Guid ForestOwnerKindId
        {
            get => forestOwnerKindId;
            set
            {
                forestOwnerKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Акт о ландшафтном пожаре")]
        public Guid? LandscapeFireActId
        {
            get => landscapeFireActId;
            set
            {
                landscapeFireActId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Зона мониторинга")]
        public Guid MonitoringZoneKindId
        {
            get => monitoringZoneKindId;
            set
            {
                monitoringZoneKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Район применения сил и средств")]
        public Guid FightFireZoneId
        {
            get => fightFireZoneId;
            set
            {
                fightFireZoneId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Авиаотделение")]
        public Guid? AirbaseDepartmentId
        {
            get => airbaseDepartmentId;
            set
            {
                airbaseDepartmentId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Покров")]
        public Guid? CoverKindId
        {
            get => coverKindId;
            set
            {
                coverKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Номер пожара по авиаотделению")]
        public int? NumberAirbaseDepartment
        {
            get => numberAirbaseDepartment;
            set
            {
                numberAirbaseDepartment = value;
                OnPropertyChanged();
            }
        }


        [DisplayName("Номер пожара по административному району")]
        public int? NumberMunicipalDistrict
        {
            get => numberMunicipalDistrict;
            set
            {
                numberMunicipalDistrict = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Номер крупного пожара")]
        public int? NumberBigFire
        {
            get => numberBigFire;
            set
            {
                numberBigFire = value;
                OnPropertyChanged();
            }
        }
        
        [DisplayName("Номер ИСДМ")]
        public int? NumberIsdm
        {
            get => numberIsdm;
            set
            {
                numberIsdm = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Азимут от авиаотделения до пожара")]
        public decimal? AzimuthAirbaseDepartment
        {
            get { return azimuthAirbaseDepartment; }
            set
            {
                azimuthAirbaseDepartment = value;
                OnPropertyChanged("AzimuthAirbaseDepartment");
            }
        }

        [DisplayName("Дальность от авиаотделения до пожара")]
        public decimal? DistanceAirbaseDepartment
        {
            get { return distanceAirbaseDepartment; }
            set
            {
                distanceAirbaseDepartment = value;
                OnPropertyChanged("DistanceAirbaseDepartment");
            }
        }

        [DisplayName("Расстояние до места высадки")]
        public decimal? DistanceLanding
        {
            get => distanceLanding;
            set
            {
                distanceLanding = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Расстояние до транспортных путей")]
        public decimal? DistanceTrafficRoad
        {
            get => distanceTrafficRoad;
            set
            {
                distanceTrafficRoad = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Дата фронтальных осадков")]
        public DateTime? FrontalPrecipitationDate
        {
            get => frontalPrecipitationDate;
            set
            {
                frontalPrecipitationDate = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Скорость ветра")]
        public decimal? WindStrength
        {
            get => windStrength;
            set
            {
                windStrength = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("КПО в день обнаружения")]
        public byte? KpoDetection
        {
            get => kpoDetection;
            set
            {
                kpoDetection = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("КПО в день ликвидации")]
        public byte? KpoLiquidation
        {
            get => kpoLiquidation;
            set
            {
                kpoLiquidation = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Стоимость услуг по найму ВС")]
        public decimal? CostRentAircraft
        {
            get => costRentAircraft;
            set
            {
                costRentAircraft = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("SettlementId")]
        public virtual SettlementOktmo? Settlement { get; set; }

        [ForeignKey("MunicipalDistrictId")]
        public virtual SettlementOktmo? MunicipalDistrict { get; set; }

        [ForeignKey("BeginReasonKindId")]
        public virtual BeginFireReasonKind? BeginReasonKind { get; set; }

        [ForeignKey("DetectionKindId")]
        public virtual DetectionKind? DetectionKind { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region? Region { get; set; }

        [DisplayName("Список динамик")]
        [Aggregation]
        public virtual ICollection<LandscapeFireDynamic> LandscapeFireDynamics { get; set; } = new List<LandscapeFireDynamic>();

        [ForeignKey("AirbaseDepartmentId")]
        public virtual AirbaseDepartment? AirbaseDepartment { get; set; }

        [ForeignKey("CoverKindId")]
        public virtual CoverKind? CoverKind { get; set; }

        [ForeignKey("FightFireZoneId")]
        public virtual FightFireZone? FightFireZone { get; set; }

        [ForeignKey("ForestOwnerKindId")]
        public virtual ForestOwnerKind? ForestOwnerKind { get; set; }

        [ForeignKey("LandscapeFireActId")]
        public virtual LandscapeFireAct? LandscapeFireAct { get; set; }

        [ForeignKey("MonitoringZoneKindId")]
        public virtual MonitoringZoneKind? MonitoringZoneKind { get; set; }
        
        public override string ToAuditString()
        {
            return "Ландшафтный пожар №" + Number;
        }
    }
}
