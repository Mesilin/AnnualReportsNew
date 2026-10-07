using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Meteo;
using Aviales.Model.Oktmo;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("Forestry", Schema = "Catalog")]
	[DisplayName("Лесничество")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class Forestry : AuditModelBase//, IKindType2
	{
		[Key]
		[DisplayName("Идентификатор лесничества")]
		public Guid ForestryId { get; set; }

		[Required]
		[MaxLength(6)]
		[IsBasic]
		[DisplayName("Код лесничества")]
		public string? Code { get; set; }

		[Required]
		[MaxLength(2)]
		[DisplayName("Код CodeLv")]
		[IsBasic]
		public string? CodeLv { get; set; }


		[Required]
		[MaxLength(3)]
		[DisplayName("Код CodeOiv")]
		[IsBasic]
		public string CodeOiv { get; set; }

		[Required]
		[IsBasic]
		[DisplayName("Наименование")]
		public string? Name { get; set; }

		[DisplayName("Площадь земель")]
		public decimal? InspectArea { get; set; }
		public Guid? ForestOwnerKindId { get; set; }
		public Guid? MonitoringZoneKindId { get; set; }
		public Guid RegionId { get; set; }
		public Guid? MunicipalDistrictIdOld { get; set; }
		public Guid? AirbaseDepartmentId { get; set; }
		
		[DisplayName("Год справочника")]
		[IsBasic]
		public int Year { get; set; }

		[IsBasic]
		[DisplayName("Активность")]
		public bool IsActive { get; set; }

		/// <summary>
		/// Резерв тушения
		/// </summary>
		public int? ExtinguishingReserve { get; set; }
		public Guid? MunicipalDistrictId { get; set; }
		//public virtual ICollection<GroundFightFireFormationWork> GroundFightFireFormationWorks {get; set;}

		[ForeignKey("AirbaseDepartmentId")]
		[DisplayName("Авиаотделение")]
		public virtual AirbaseDepartment? AirbaseDepartment { get; set; }

		[ForeignKey("ForestOwnerKindId")]
		[DisplayName("Принадлежность земель")]
		public virtual ForestOwnerKind? ForestOwnerKind { get; set; }

		[DisplayName("Зона мониторинга")]
		[ForeignKey("MonitoringZoneKindId")]
		public virtual MonitoringZoneKind? MonitoringZoneKind { get; set; }

		[DisplayName("Муниципальный район")]
		[ForeignKey("MunicipalDistrictId")]
		public virtual SettlementOktmo? MunicipalDistrict { get; set; }

		[DisplayName("Муниципальный район(архивное поле)")]
		[ForeignKey("MunicipalDistrictIdOld")]
		public virtual MunicipalDistrict? MunicipalDistrictOld { get; set; }

		[DisplayName("Субъект")]
		[ForeignKey("RegionId")]
		public virtual Region Region { get; set; }

		[Aggregation]
		[DisplayName("Список участковых лесничеств")]
		public virtual ICollection<ForestryDistrict> ForestryDistricts { get; set; }

		[DisplayName("Метеостанции лесничества")]
		public virtual ICollection<MeteostationForestry> MeteostationForestries { get; set; }

		//      [DisplayName("Детальный план профилактического выжигания")]
		//public virtual ICollection<PreventiveBurningDetailedPlan> PreventiveBurningDetailedPlans {get; set;}
		public override string ToAuditString()
		{
			return Name;
		}
	}
}