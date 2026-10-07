using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Oktmo
{

	[Table("MunicipalDistrict", Schema = "Oktmo")]
	[DisplayName("Административный район")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class MunicipalDistrict : AuditModelBase//, IHaveName
	{
        [Key]
		public Guid MunicipalDistrictId {get; set;}

		public Guid RegionId {get; set;}
		public Guid? OktmoId {get; set;}

		[Required]
		[MaxLength(30)]
		public string? Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public int Year {get; set;}
		public bool IsActive {get; set;}
		public virtual ICollection<Forestry> Forestries {get; set;} = new List<Forestry>();
        public virtual ICollection<ForestryDistrict> ForestryDistricts {get; set;} = new List<ForestryDistrict>();

        [ForeignKey("RegionId")]
		public virtual Region Region {get; set;} = null!;

		//public virtual ICollection<Settlement> Settlements {get; set;}
	}
}