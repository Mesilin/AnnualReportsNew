
using Aviales.Model.Aviales;
using Aviales.Model.Catalog;
using Aviales.Model.Flight;
using Aviales.Model.ForestFire;
using Aviales.Model.GeoObjects;
using Aviales.Model.Input;
using Aviales.Model.LandscapeFire;
using Aviales.Model.Meteo;
using Aviales.Model.Navigation;
using Aviales.Model.Oktmo;
using Aviales.Model.View;
using Microsoft.EntityFrameworkCore;
using UIP.Database;

namespace Aviales.Model
{
    /// <summary>
    /// Контекст данных систем типа Aviales
    /// </summary>
    public class AvialesDbContext : BaseDbContext
    {
        public AvialesDbContext(DbContextOptions<AvialesDbContext> options) : base(options)
        { }

        public DbSet<Airbase> Airbases { get; set; }
        public DbSet<AirbaseDepartment> AirbaseDepartments { get; set; }
        public DbSet<AircraftContract> AircraftContracts { get; set; }
        public DbSet<AircraftKind> AircraftKinds { get; set; }
        public DbSet<AircraftModel> AircraftModels { get; set; }
        public DbSet<AircraftPerformance> AircraftPerformances { get; set; }
        public DbSet<AircraftPerformancePlan> AircraftPerformancePlans { get; set; }
        public DbSet<AircraftPerformanceReport> AircraftPerformanceReports { get; set; }
        public DbSet<AttractedAircraft> AttractedAircrafts { get; set; }
        public DbSet<BeginFireReasonKind> BeginFireReasonKinds { get; set; }
        public DbSet<CitizenAppeal> CitizenAppeals { get; set; }
        public DbSet<CitizenAppealKind> CitizenAppealKinds { get; set; }
        public DbSet<Consequence> Consequences { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CoverKind> CoverKinds { get; set; }
        public DbSet<DeliveryTransportType> DeliveryTransportTypes { get; set; }
        public DbSet<DetectionKind> DetectionKinds { get; set; }
        public DbSet<FederalDistrict> FederalDistricts { get; set; }
        public DbSet<FightFireForceAvailability> FightFireForceAvailabilities { get; set; }
        public DbSet<FightFireForceAvailabilityByFormation> FightFireForceAvailabilityByFormations { get; set; }
        public DbSet<FightFire2024ForceAvailabilityByFormation> FightFire2024ForceAvailabilityByFormations { get; set; }
        public DbSet<FightFireForceAvailabilityByFormationReport> FightFireForceAvailabilityByFormationReports { get; set; }
        public DbSet<FightFire2024ForceAvailabilityByFormationReport> FightFire2024ForceAvailabilityByFormationReports { get; set; }
        public DbSet<FightFireForceKind> FightFireForceKinds { get; set; }
        public DbSet<FightFireFormationKind> FightFireFormationKinds { get; set; }
        public DbSet<FightFireFormationResourceKind> FightFireFormationResourceKinds { get; set; }
        public DbSet<FightFireFormationTeamKind> FightFireFormationTeamKinds { get; set; }
        public DbSet<FightFireResource> FightFireResources { get; set; }
        public DbSet<FightFireResourceKind> FightFireResourceKinds { get; set; }
        public DbSet<FightFireResourceLocal> FightFireResourceLocals { get; set; }
        public DbSet<FightFireTeam> FightFireTeams { get; set; }
        public DbSet<FightFireTeamKind> FightFireTeamKinds { get; set; }
        public DbSet<FightFireTeamLocal> FightFireTeamLocals { get; set; }
        public DbSet<FightFireZone> FightFireZones { get; set; }
        public DbSet<Fire> Fires { get; set; }
        public DbSet<FireView> FireViews { get; set; }

        public DbSet<FireAnalytics> FireAnalyticses { get; set; }
        public DbSet<FireDamage> FireDamages { get; set; }
        public DbSet<FireDamageType> FireDamageTypes { get; set; }
        public DbSet<FireDynamic> FireDynamics { get; set; }
        public DbSet<FireDynamicQuarter> FireDynamicQuarters { get; set; }
        public DbSet<FireDynamicStateKind> FireDynamicStateKinds { get; set; }
        public DbSet<FireFlight> FireFlights { get; set; }
        public DbSet<FireFlightAircraft> FireFlightAircrafts { get; set; }
        public DbSet<FireFlightInspection> FireFlightInspections { get; set; }
        public DbSet<FireFlightPaymentGroup> FireFlightPaymentGroups { get; set; }
        public DbSet<FireFlightPaymentRate> FireFlightPaymentRates { get; set; }
        public DbSet<FireFlightPersonnel> FireFlightPersonnels { get; set; }
        public DbSet<FireFlightWork> FireFlightWorks { get; set; }
        public DbSet<FireFlightWorkCost> FireFlightWorkCosts { get; set; }
        public DbSet<FireFlightWorkKind> FireFlightWorkKinds { get; set; }
        public DbSet<FireHazard> FireHazards { get; set; }
        public DbSet<FireHazardConsequence> FireHazardConsequences { get; set; }
        public DbSet<FireHazardDistrict> FireHazardDistricts { get; set; }
        public DbSet<FireHazardKind> FireHazardKinds { get; set; }
        public DbSet<FireIntensityKind> FireIntensityKinds { get; set; }
        public DbSet<FireKind> FireKinds { get; set; }
        public DbSet<FirePrevention> FirePreventions { get; set; }
        public DbSet<FirePreventionAndSafetyRegulationViolation> FirePreventionAndSafetyRegulationViolations { get; set; }
        public DbSet<FireSafetyRegulationViolation> FireSafetyRegulationViolations { get; set; }

        public DbSet<ForestKind> ForestKinds { get; set; }
        public DbSet<ForestOwnerKind> ForestOwnerKinds { get; set; }
        public DbSet<ForestOwnerKind2ForestKind> ForestOwnerKind2ForestKinds { get; set; }
        public DbSet<Forestry> Forestries { get; set; }
        public DbSet<ForestryDistrict> ForestryDistricts { get; set; }
        public DbSet<ForestryTract> ForestryTracts { get; set; }

        public DbSet<GroundFireControl> GroundFireControls { get; set; }
        public DbSet<GroundKind> GroundKinds { get; set; }
        public DbSet<IsdmHotSpot> IsdmHotSpots { get; set; }
        public DbSet<IsdmHotSpotExclusionReasonKind> IsdmHotSpotExclusionReasonKinds { get; set; }
        public DbSet<IsdmHotSpotStateKind> IsdmHotSpotStateKinds { get; set; }
        public DbSet<IsdmHotSpotZoneKind> IsdmHotSpotZoneKinds { get; set; }
        public DbSet<Leaseholder> Leaseholders { get; set; }
        public DbSet<LeaseholderLeaseKind> LeaseholderLeaseKinds { get; set; }
        public DbSet<LeaseKind> LeaseKinds { get; set; }

        public DbSet<MeteoInfo> MeteoInfoes { get; set; }
        public DbSet<MeteoInfoHourly> MeteoInfoHourlies { get; set; }
        public DbSet<Meteoscale> Meteoscales { get; set; }
        public DbSet<Meteostation> Meteostations { get; set; }
        public DbSet<MeteostationAirbaseDepartment> MeteostationAirbaseDepartments { get; set; }
        public DbSet<MeteostationForestry> MeteostationForestries { get; set; }
        public DbSet<MeteostationMeteoscale> MeteostationMeteoscales { get; set; }
        public DbSet<MeteostationRegion> MeteostationRegions { get; set; }
        public DbSet<MeteostationSettlementOktmo> MeteostationSettlementOktmos { get; set; }
        public DbSet<MonitoringZoneKind> MonitoringZoneKinds { get; set; }
        public DbSet<MunicipalDistrict> MunicipalDistricts { get; set; }
        public DbSet<NotLandingReasonKind> NotLandingReasonKinds { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<PersonnelDynamic> PersonnelDynamics { get; set; }
        public DbSet<PlantationKind> PlantationKinds { get; set; }
        public DbSet<Region> Regions { get; set; }
		public DbSet<FireDangerousPeriod> FireDangerousPeriods { get; set; }
        public DbSet<SettlementKind> SettlementKinds { get; set; }
        public DbSet<SettlementOktmo> SettlementOktmos { get; set; }
        public DbSet<SpeciesKind> SpeciesKinds { get; set; }

        public DbSet<Transport> Transports { get; set; }
        public DbSet<VulnerableObject> VulnerableObjects { get; set; }
        public DbSet<ThreatCondition> ThreatConditions { get; set; }
        public DbSet<WorkPosition> WorkPositions { get; set; }
        public DbSet<WorkPosition2WorkPositionGroup> WorkPosition2WorkPositionGroups { get; set; }
        public DbSet<WorkPositionGroup> WorkPositionGroups { get; set; }
        public DbSet<WorkPositionGroupKind> WorkPositionGroupKinds { get; set; }
        public DbSet<ZoneDependencies> ZoneDependencieses { get; set; }

        // FireHazard
        public DbSet<FireHazardAct> FireHazardActs { get; set; }
        public DbSet<FireHazardActRow> FireHazardActRows { get; set; }

        // GeoObjects
        public DbSet<GasFlare> GasFlares { get; set; }
        public DbSet<HydroLine> HydroLines { get; set; }
        public DbSet<HydroPolygon> HydroPolygons { get; set; }
        public DbSet<OilGasObject> OilGasObjects { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<CondensateLine> CondensateLines { get; set; }
        public DbSet<GasPipeLine> GasPipeLines { get; set; }
        public DbSet<OilPipeLine> OilPipeLines { get; set; }
        public DbSet<ProductPipeLine> ProductPipeLines { get; set; }

        public DbSet<NearestObject> NearestObjects { get; set; }
        public DbSet<ImportantObjectType> ImportantObjectType { get; set; }

        public DbSet<AirPatrolRoutes> AirPatrolRoutes { get; set; }

        // LandscapeFireAct

        public DbSet<LandscapeFireAct> LandscapeFireActs { get; set; }
        public DbSet<LandscapeFireActCulprit> LandscapeFireActCulprits { get; set; }
        public DbSet<LandscapeFireActExtinguishingMethod> LandscapeFireActExtinguishingMethods { get; set; }

        // LandscapeFire

        public DbSet<LandscapeFire.LandscapeFire> LandscapeFires { get; set; }
        public DbSet<LandscapeFireDynamic> LandscapeFireDynamics { get; set; }

        // Виновники пожаров
        public DbSet<CulpritFire> CulpritFires { get; set; }

        // Настройки годовой отчётности
        public DbSet<AnnualReportSetting> AnnualReportSettings { get; set; }

        public DbSet<SoilCover> SoilCovers { get; set; }

        #region Navigation

        public DbSet<Route> Routes { get; set; }
        public DbSet<Route2Point> Route2Points { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }
        public DbSet<RouteSegment> RouteSegments { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<MarkEventType> MarkEventTypes { get; set; }
        public DbSet<MarkPoint> MarkPoints { get; set; }
        public DbSet<GpsPoint> GpsPoints { get; set; }
        public DbSet<Abonent> Abonents { get; set; }
        public DbSet<AbonentChannel> AbonentChannels { get; set; }
        public DbSet<AbonentGroup> AbonentGroups { get; set; }
        public DbSet<AbonentGroupImage> AbonentGroupImage { get; set; }
        public DbSet<AbonentSyncInfo> AbonentSyncInfoes { get; set; }
        public DbSet<FlightLog> FlightLogs { get; set; }
        public DbSet<FlightLogLandingPoint> FlightLogLandingPoints { get; set; }
        public DbSet<FlightLogWorkPlan> FlightLogWorkPlans { get; set; }
        //public DbSet<TklFlightArea> TklFlightAreas { get; set; }

        #endregion Navigation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {

            base.ConfigureConventions(configurationBuilder);
            // Меняем точномть DateTime в Авиалесе
            // Пример в SQL:
            // ALTER TABLE "ForestFire"."Fire"
            // ALTER COLUMN "Modified" TYPE TIMESTAMPTZ(3);
            configurationBuilder.Properties<DateTime>().HavePrecision(3);
            configurationBuilder.Properties<DateTime?>().HavePrecision(3);
        }

    }
}
