using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Catalog
{

	[Table("ForestryTract", Schema = "Catalog")]
	[DisplayName("Урочище")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class ForestryTract : AuditModelBase, IProductionDependent
	{
        [DisplayName("Идентификатор урочища")]
		[Key]
        public Guid ForestryTractId {get; set;}
        [DisplayName("Идентификатор участкового лесничества")]

        public Guid? ProductionId { get; set; }
        [DisplayName("Идентификатор внедрения")]

		public Guid ForestryDistrictId {get; set;}

        [DisplayName("Наименование урочища")]
		[Required]
        [MaxLength(200)]
		public string? Name {get; set;}

        [DisplayName("Год справочника")]
        public int Year {get; set;}

        [DisplayName("Активность")]
        public bool IsActive {get; set;}

        [DisplayName("Участковое лесничество")]
        [ForeignKey("ForestryDistrictId")]
        public virtual ForestryDistrict ForestryDistrict { get; set; } = null!;

// TODO: Anokhin Alexander Mesilin Evgeniy::Нужно ли это::2024-08-15
        [DisplayName("Список кварталов/выделов")]
		public virtual ICollection<FireDynamicQuarter> FireDynamicQuarters {get; set;} = new List<FireDynamicQuarter>();

        public override string ToAuditString()
        {
            return Name;
        }
    }
}