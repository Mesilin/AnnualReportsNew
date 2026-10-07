using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FederalDistrict", Schema = "Catalog")]
    [DisplayName("Федеральный округ")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FederalDistrict : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FederalDistrictId { get; set; }

        [Required]
        [MaxLength(3)]
        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Порядок сортировки")]
        public int? Sorting { get; set; }

        [DisplayName("Год справочника")]
        public int Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Идентификатор страны")]
        public Guid? CountryId { get; set; }

        [DisplayName("Список субъектов")]
        public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

        [DisplayName("Список населенных пунктов")]
        public virtual ICollection<SettlementOktmo> SettlementOktmoes { get; set; } = new List<SettlementOktmo>();

        [ForeignKey("CountryId")]
        [DisplayName("Страна")]
        public virtual Country? Country { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}