using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Aviales.Model.Meteo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{

    [Table("AirbaseDepartment", Schema = "Catalog")]
    [DisplayName("Авиаотделения")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
    [AssociativeToString("{ShortName} , {Year} г.")]
	public partial class AirbaseDepartment : AuditModelBase, IProductionDependent
    {
        private string? code;
        private string? name;
        private string? shortName;
        private double? latitude;
        private double? longitude;
        private decimal? inspectArea;
        private int year;
        private Guid? productionId;

        [DisplayName("Идентификатор авиаотделения")]
        [Key]
        public Guid AirbaseDepartmentId { get; set; }

        [DisplayName("Идентификатор авиабазы")]
        public Guid? AirbaseId { get; set; }

        [Required]
        [DisplayName("Код")]
        [MaxLength(6)]
        public string? Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Наименование авиаотделения")]
        [Required]
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Краткое наименование авиаотделения")]
        public string? ShortName
        {
            get { return shortName; }
            set
            {
                shortName = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Широта")]
        public double? Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Долгота")]
        public double? Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Площадь контролируемых лесов")]
        public decimal? InspectArea
        {
            get { return inspectArea; }
            set
            {
                inspectArea = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Год справочника")]
        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId
        {
            get { return productionId; }
            set
            {
                if (productionId == value)
                    return;

                productionId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Активность")]

        public bool IsActive { get; set; }

        [DisplayName("Авиабаза")]
        [ForeignKey("AirbaseId")]
        public virtual Airbase? Airbase { get; set; }

        [DisplayName("Список лесничеств")]
        public virtual ICollection<Forestry> Forestries { get; set; } = new List<Forestry>();
        public virtual ICollection<Fire>? Fires { get; set; }
        [DisplayName("Список участковых лесничеств")]
        public virtual ICollection<ForestryDistrict> ForestryDistricts { get; set; } = new List<ForestryDistrict>();

        [DisplayName("Список метеостанций")]
        public virtual ICollection<MeteostationAirbaseDepartment> MeteostationAirbaseDepartments { get; set; } = new List<MeteostationAirbaseDepartment>();

        [NotMapped]
        public string? LatitudeString { get; set; }
        [NotMapped]
        public string? LongitudeString { get; set; }

        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return ShortName + ", " + Year + " г.";
        }
    }
}