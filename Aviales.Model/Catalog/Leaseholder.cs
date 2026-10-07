using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Catalog
{

	[Table("Leaseholder", Schema = "Catalog")]
	[DisplayName("Арендатор")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class Leaseholder : AuditModelBase, IProductionDependent
	{
        [Key]
		[DisplayName("Идентификатор")]
		public Guid LeaseholderId {get; set;}

		[DisplayName("Идентификатор лесничества")]
        public Guid? ForestryId { get; set; }

		[DisplayName("Идентификатор участкового лесничества")]
        public Guid? ForestryDistrictId { get; set; }

        [Required]
		[DisplayName("Наименование")]
		public string? Name {get; set;}

        [DisplayName("Идентификатор внедрения")]
		public Guid? ProductionId { get; set; }

		[DisplayName("Год справочника")]
		public int Year {get; set;}
		
        public virtual ICollection<LeaseholderLeaseKind> LeaseholderLeaseKinds { get; set; }

        [ForeignKey("ForestryId")]
		[DisplayName("Лесничество")]
        public virtual Forestry? Forestry { get; set; }

		[DisplayName("Участковое лесничество")]
        [ForeignKey("ForestryDistrictId")]
        public virtual ForestryDistrict? ForestryDistrict { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}