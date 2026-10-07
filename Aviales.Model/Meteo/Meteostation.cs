using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Meteo
{

    [Table("Meteostation", Schema = "Meteo")]
    [DisplayName("Метеостанция")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class Meteostation : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid MeteostationId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        [MaxLength(500)]
        public string? Name { get; set; }

        [DisplayName("Широта")]
        public double? Latitude { get; set; }

        [DisplayName("Долгота")]
        public double? Longitude { get; set; }

        [DisplayName("Год")]
        public int? Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Метеоинформация")]
        [Aggregation]
        public virtual ICollection<MeteoInfo> MeteoInfoes { get; set; } = new List<MeteoInfo>();

        [DisplayName("Метеоинформация почасовая")]
        [Aggregation]
        public virtual ICollection<MeteoInfoHourly> MeteoInfoHourlies { get; set; } = new List<MeteoInfoHourly>();

        [DisplayName("Авиаотделения")]
        [Aggregation]
        public virtual ICollection<MeteostationAirbaseDepartment> MeteostationAirbaseDepartments { get; set; } = new List<MeteostationAirbaseDepartment>();

        [DisplayName("Лесничества")]
        [Aggregation]
        public virtual ICollection<MeteostationForestry> MeteostationForestries { get; set; } = new List<MeteostationForestry>();

        [DisplayName("Метеошкалы")]
        public virtual ICollection<MeteostationMeteoscale> MeteostationMeteoscales { get; set; } = new List<MeteostationMeteoscale>();

        [DisplayName("Субъекты")]
        [Aggregation]
        public virtual ICollection<MeteostationRegion> MeteostationRegions { get; set; } = new List<MeteostationRegion>();

        [DisplayName("Районы")]
        [Aggregation]
        public virtual ICollection<MeteostationSettlementOktmo> MeteostationSettlementOktmos { get; set; } = new List<MeteostationSettlementOktmo>();

        [DisplayName("Внедрение")]
        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}