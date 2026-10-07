using Aviales.Model.ForestFire;
using Incom.Common2.Persistence;

namespace Aviales.Model.Summary
{
    public class FireSummary : ModelBase
    {
        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public Guid RegionId { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string? RegionName { get; set; }

        /// <summary>
        /// Идентификатор пожара
        /// </summary>
        public Guid FireId { get; set; }

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
        /// Участковое лесничество
        /// </summary>
        public string? ForestryDistrictName { get; set; }

        /// <summary>
        /// Идентификатор принадлежности
        /// </summary>
        public Guid ForestOwnerKindId { get; set; }

        /// <summary>
        /// Идентификатор зоны
        /// </summary>
        public Guid MonitoringZoneKindId { get; set; }

        /// <summary>
        /// Идентификатор административного района
        /// </summary>
        public Guid? MunicipalDistrictId { get; set; }

        /// <summary>
        /// Административный район
        /// </summary>
        public string? MunicipalDistrictName { get; set; }

        /// <summary>
        /// Идентификатор ближайшего населенного пункта
        /// </summary>
        public Guid? SettlementId { get; set; }

        /// <summary>
        /// Ближайший населенный пункт
        /// </summary>
        public string? SettlementName { get; set; }

        /// <summary>
        /// Идентификатор породы
        /// </summary>
        public Guid? SpeciesKindId { get; set; }

        /// <summary>
        /// Порода
        /// </summary>
        public string? SpeciesKindName { get; set; }

        /// <summary>
        /// Идентификатор покрова
        /// </summary>
        public Guid? CoverKindId { get; set; }

        /// <summary>
        /// Покров
        /// </summary>
        public string? CoverKindName { get; set; }

        /// <summary>
        /// Идентификатор "Целевое назначение лесов"
        /// </summary>
        public Guid? ForestKindId { get; set; }

        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public string? ForestKindName { get; set; }

        /// <summary>
        /// Идентификатор категории земель
        /// </summary>
        public Guid? GroundKindId { get; set; }

        /// <summary>
        /// Категория земель
        /// </summary>
        public string? GroundKindName { get; set; }

        /// <summary>
        /// Идентификатор причины пожаров
        /// </summary>
        public Guid? BeginFireReasonKindId { get; set; }

        /// <summary>
        /// Причины пожара
        /// </summary>
        public string? BeginFireReasonKindName { get; set; }

        /// <summary>
        /// Идентификатор способа обнаружения
        /// </summary>
        public Guid? DetectionKindId { get; set; }

        /// <summary>
        /// Cпособ обнаружения
        /// </summary>
        public string? DetectionKindName { get; set; }

        /// <summary>
        /// Идентификаторы арендатора
        /// </summary>
        public List<Guid>? LeaseholderIds { get; set; }

        /// <summary>
        /// Арендатор
        /// </summary>
        public List<string>? LeaseholderNames { get; set; }

        /// <summary>
        /// Зона мониторинга
        /// </summary>
        public string? MonitoringZoneKindName { get; set; }

        /// <summary>
        /// Принадлежность леса
        /// </summary>
        public string? ForestOwnerKindName { get; set; }

        /// <summary>
        /// КПО обнаружения
        /// </summary>
        public int? KpoDetection { get; set; }

        /// <summary>
        /// КПО ликвидации
        /// </summary>
        public int? KpoLiquidation { get; set; }

        /// <summary>
        /// Номер лесничества
        /// </summary>
        public int NumberForestry { get; set; }

        /// <summary>
        /// Название лесничества
        /// </summary>
        public string? ForestryName { get; set; }

        /// <summary>
        /// Номер авиаотделения
        /// </summary>
        public int? NumberAirbaseDepartment { get; set; }

        /// <summary>
        /// Название авиаотделения
        /// </summary>
        public string? AirbaseDepartmentName { get; set; }

        /// <summary>
        /// Номер большого пожара
        /// </summary>
        public int? NumberBigFire { get; set; }

        /// <summary>
        /// Номер пожара по ИСДМ
        /// </summary>
        public int? NumberIsdm { get; set; }

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
        /// Год 
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Первая динамика
        /// </summary>
        public FirstFireDynamic FirstFireDynamic
        {
            get { return firstFireDynamic; }
            set { firstFireDynamic = value; }
        }

        /// <summary>
        /// Дата обнаружения
        /// </summary>
        public DateTime? FirstFireDynamicDate
        {
            get { return firstFireDynamic != null ? firstFireDynamic.FireDynamicDate : (DateTime?)null; }
        }

        /// <summary>
        /// Покрытая площадь при обнаружении
        /// </summary>
        public decimal? CoverAreaFirstFireDynamic
        {
            get { return firstFireDynamic != null ? firstFireDynamic.CoverArea : null; }
        }

        /// <summary>
        /// Непокрытая площадь при обнаружении
        /// </summary>
        public decimal? NoncoverAreaFirstFireDynamic
        {
            get { return firstFireDynamic != null ? firstFireDynamic.NoncoverArea : null; }
        }

        /// <summary>
        /// Нелесная площадь при обнаружении
        /// </summary>
        public decimal? NonforestAreaFirstFireDynamic
        {
            get { return firstFireDynamic != null ? firstFireDynamic.NonforestArea : null; }
        }

        /// <summary>
        /// Последняя динамика
        /// </summary>
        public LastFireDynamic LastFireDynamic
        {
            get { return lastFireDynamic; }
            set { lastFireDynamic = value; }
        }

        /// <summary>
        /// Дата последнего осмотра
        /// </summary>
        public DateTime? LastFireDynamicDate
        {
            get { return lastFireDynamic != null ? lastFireDynamic.FireDynamicDate : (DateTime?)null; }
        }

        /// <summary>
        /// Покрытая площадь
        /// </summary>
        public decimal? CoverAreaLastFireDynamic
        {
            get { return lastFireDynamic != null ? lastFireDynamic.CoverArea : null; }
        }

        /// <summary>
        /// Непокрытая площадь
        /// </summary>
        public decimal? NoncoverAreaLastFireDynamic
        {
            get { return lastFireDynamic != null ? lastFireDynamic.NoncoverArea : null; }
        }

        /// <summary>
        /// Нелесная площадь
        /// </summary>
        public decimal? NonforestAreaLastFireDynamic
        {
            get { return lastFireDynamic != null ? lastFireDynamic.NonforestArea : null; }
        }

        /// <summary>
        /// Верховая площадь
        /// </summary>
        public decimal? CrownAreaLastFireDynamic
        {
            get { return lastFireDynamic != null ? lastFireDynamic.CrownArea : null; }
        }

        /// <summary>
        /// Почвенная площадь
        /// </summary>
        public decimal? GroundAreaLastFireDynamic
        {
            get { return lastFireDynamic != null ? lastFireDynamic.GroundArea : null; }
        }

        /// <summary>
        /// Квартал, выдел
        /// </summary>
        public string? QuartersAndStratums
        {
            get
            {
                if (lastFireDynamic != null && lastFireDynamic.QuartersAndStratums.Any())
                {
                    var quarters = new List<string>();
                    var stratums = new List<string>();

                    foreach (var qs in lastFireDynamic.QuartersAndStratums)
                    {
                        if (!string.IsNullOrEmpty(qs.Quarter) && !quarters.Contains(qs.Quarter))
                            quarters.Add(qs.Quarter);

                        if (!string.IsNullOrEmpty(qs.Stratum) && !stratums.Contains(qs.Stratum))
                            stratums.Add(qs.Stratum);
                    }

                    var quartersStr = string.Empty;
                    var stratumsStr = string.Empty;

                    if (quarters.Count > 0)
                    {
                        quartersStr = string.Join("; ", quarters);
                        quartersStr = "кв. " + quartersStr;
                    }

                    if (stratums.Count > 0)
                    {
                        stratumsStr = string.Join("; ", stratums);
                        stratumsStr = " (выд. " + stratumsStr + ")";
                    }

                    return quartersStr + stratumsStr;
                }

                return string.Empty;
            }
        }

        private FirstFireDynamic firstFireDynamic;
        private LastFireDynamic lastFireDynamic;
    }

    public class FirstFireDynamic
    {
        public DateTime FireDynamicDate { get; set; }

        public decimal? NoncoverArea { get; set; }

        public decimal? CoverArea { get; set; }

        public decimal? NonforestArea { get; set; }
    }

    public class LastFireDynamic
    {
        public decimal? CoverArea { get; set; }

        public decimal? NoncoverArea { get; set; }

        public decimal? NonforestArea { get; set; }

        public decimal? CrownArea { get; set; }

        public decimal? GroundArea { get; set; }

        public DateTime FireDynamicDate { get; set; }

        public IEnumerable<QuartersAndStratums> QuartersAndStratums { get; set; }


    }

    public class QuartersAndStratums
    {
        public string? Quarter { get; set; }

        public string? Stratum { get; set; }

    }

    public static class FireSummaryExtension
    {
        public static IQueryable<FireSummary> SelectFireSummaries(this IQueryable<Fire> queryable)
        {
            return queryable.Where(a => !a.IsDeleted).Select(s => new FireSummary
            {
                LastFireDynamic = s.FireDynamics.OrderByDescending(o => o.FireDynamicDate).Select(ss => new LastFireDynamic
                {
                    CoverArea = ss.CoverArea,
                    NoncoverArea = ss.NoncoverArea,
                    NonforestArea = ss.NonforestArea,
                    CrownArea = ss.CrownArea,
                    GroundArea = ss.UndergroundArea,
                    FireDynamicDate = ss.FireDynamicDate,
                    QuartersAndStratums = ss.FireDynamicQuarters.Select(sss => new QuartersAndStratums
                    {
                        Quarter = sss.Quarter,
                        Stratum = sss.Stratum
                    })
                }).FirstOrDefault(),

                FirstFireDynamic = s.FireDynamics.OrderBy(o => o.FireDynamicDate).Select(ss => new FirstFireDynamic
                {
                    FireDynamicDate = ss.FireDynamicDate,
                    NoncoverArea = ss.NoncoverArea,
                    CoverArea = ss.CoverArea,
                    NonforestArea = ss.NonforestArea
                }).FirstOrDefault(),

                RegionId = s.AirbaseDepartment == null ? Guid.Empty : s.AirbaseDepartment.Airbase.RegionId,
                RegionName = s.AirbaseDepartment == null ? string.Empty : s.AirbaseDepartment.Airbase.Region.Name,
                FireId = s.FireId,
                AirbaseDepartmentId = s.AirbaseDepartment == null ? Guid.Empty : s.AirbaseDepartment.AirbaseDepartmentId,
                ForestryId = s.ForestryId,
                ForestryDistrictId = s.ForestryDistrict == null ? Guid.Empty : s.ForestryDistrict.ForestryDistrictId,
                ForestryDistrictName = s.ForestryDistrict == null ? string.Empty : s.ForestryDistrict.Name,
                ForestOwnerKindId = s.ForestOwnerKindId,
                MonitoringZoneKindId = s.MonitoringZoneKindId,
                MunicipalDistrictId = s.MunicipalDistrict == null ? Guid.Empty : s.MunicipalDistrict.SettlementOktmoId,
                MunicipalDistrictName = s.MunicipalDistrict == null ? string.Empty : s.MunicipalDistrict.Name2,
                SettlementId = s.Settlement == null ? Guid.Empty : s.Settlement.SettlementOktmoId,
                SettlementName = s.Settlement == null ? string.Empty : s.Settlement.ShortName,
                SpeciesKindId = s.SpeciesKind == null ? Guid.Empty : s.SpeciesKind.SpeciesKindId,
                SpeciesKindName = s.SpeciesKind == null ? string.Empty : s.SpeciesKind.ShortName,
                CoverKindId = s.CoverKindId,
                NumberIsdm = s.NumberIsdm,
                CoverKindName = s.CoverKind == null ? string.Empty : s.CoverKind.ShortName,
                ForestKindId = s.ForestKind == null ? Guid.Empty : s.ForestKind.ForestKindId,
                ForestKindName = s.ForestKind == null ? string.Empty : s.ForestKind.ShortName,
                GroundKindId = s.GroundKind == null ? Guid.Empty : s.GroundKind.GroundKindId,
                GroundKindName = s.GroundKind == null ? string.Empty : s.GroundKind.ShortName,
                BeginFireReasonKindId = s.BeginFireReasonKind == null ? Guid.Empty : s.BeginFireReasonKind.BeginFireReasonKindId,
                BeginFireReasonKindName = s.BeginFireReasonKind == null ? string.Empty : s.BeginFireReasonKind.ShortName,
                DetectionKindId = s.DetectionKind == null ? Guid.Empty : s.DetectionKind.DetectionKindId,
                DetectionKindName = s.DetectionKind == null ? string.Empty : s.DetectionKind.ShortName,
                LeaseholderIds = s.FireLeaseholders == null ? new List<Guid>() : s.FireLeaseholders.Select(s=>s.LeaseholderId).ToList(),
                LeaseholderNames = s.FireLeaseholders == null ? new List<string>() : s.FireLeaseholders.Select(s => s.Leaseholder.Name).ToList(),
				MonitoringZoneKindName = s.MonitoringZoneKind == null ? string.Empty : s.MonitoringZoneKind.ShortName,
                ForestOwnerKindName = s.ForestOwnerKind == null ? string.Empty : s.ForestOwnerKind.ShortName2,
                KpoDetection = s.KpoDetection,
                KpoLiquidation = s.KpoLiquidation,
                NumberForestry = s.NumberForestry,
                ForestryName = s.Forestry == null ? string.Empty : s.Forestry.Name,
                NumberAirbaseDepartment = s.NumberAirbaseDepartment,
                AirbaseDepartmentName = s.AirbaseDepartment == null ? string.Empty : s.AirbaseDepartment.ShortName,
                NumberBigFire = s.NumberBigFire,
                FireFightBeginDate = s.FireFightBeginDate,
                FirstMessageDate = s.FirstMessageDate,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                Year = s.Year,
                AzimuthSettlement = s.AzimuthSettlement,
                AzimuthAirbaseDepartment = s.AzimuthAirbaseDepartment,
                DistanceSettlement = s.DistanceSettlement,
                DistanceAirbaseDepartment = s.DistanceAirbaseDepartment,
                DistanceLanding = s.DistanceLanding,
                DistanceTrafficRoad = s.DistanceTrafficRoad,
                WindStrength = s.WindStrength,
                FrontalPrecipitationDate = s.FrontalPrecipitationDate,
                CostTotal = s.CostTotal,
                CostRentAircraft = s.CostRentAircraft,
                CostDamage = s.CostDamage,
                ProtocolDate = s.ProtocolDate,
                ProtocolNumber = s.ProtocolNumber,
                Description = s.Description
            });
        }
    }
}
