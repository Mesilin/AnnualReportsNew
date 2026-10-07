using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("Airbase", Schema = "Catalog")]
    [DisplayName("Авиабаза")]
    [Serializable]
    [AssociativeToString("{ShortName} , {Year} г.")]
	public partial class Airbase : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор авиабазы")]
        public Guid AirbaseId { get; set; }

        [Required]
        [DisplayName("Код авиабазы")]
        [MaxLength(6)]
        public string? Code { get; set; }

        [DisplayName("Наименование авиабазы")]
        [Required]
        public string? Name { get; set; }

        [DisplayName("Краткое наименование авиабазы")]
        public string? ShortName { get; set; }
        
        [DisplayName("Широта")]
        public double? Latitude { get; set; }
        
        [DisplayName("Долгота")]
        public double? Longitude { get; set; }

        [DisplayName("Площадь контролируемых лесов")]
        public decimal? InspectArea { get; set; }

        [DisplayName("Год справочника")]
        public int Year { get; set; }

        [DisplayName("Идентификатор субъекта")]
        public Guid RegionId { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Субъект")]
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
        
        [Aggregation]
        [DisplayName("Список авиаотделений")]
        public virtual ICollection<AirbaseDepartment> AirbaseDepartments { get; set; } = new List<AirbaseDepartment>();

        [NotMapped]
        public string? LatitudeString { get; set; }

        [NotMapped]
        public string? LongitudeString { get; set; }

        public override string ToAuditString()
        {
            return Name + ", " + Year + " г.";
        }
    }
}