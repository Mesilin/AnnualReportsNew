using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	[Table("FireHazardDistrict", Schema = "Aviales")]
	[DisplayName("Пожароопасный район")]
	[Serializable]
	[AssociativeToString("Пожароопасный район {MunicipalDistrict} согласно: {FireHazard}")]
	public partial class FireHazardDistrict : AuditModelBase
	{
		[Key]
		public Guid FireHazardDistrictId {get; set;}

		public Guid FireHazardId {get; set;}
		public Guid MunicipalDistrictId {get; set;}

		[ForeignKey("MunicipalDistrictId")]
		public virtual SettlementOktmo MunicipalDistrict {get; set;} = null!;

		[ForeignKey("FireHazardId")]
		public virtual FireHazard FireHazard {get; set;} =null!;
	}
}