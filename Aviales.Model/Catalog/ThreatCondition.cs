using Aviales.Model.ForestFire;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aviales.Model.Catalog
{
	[Table("ThreatCondition", Schema = "ForestFire")]
	[Description("Действующие угрозы")]
	public class ThreatCondition : ModelBase, IProductionDependent
	{
		[Key]
		[DisplayName("Идентификатор")]
		public Guid ThreatConditionId { get; set; }

		[DisplayName("Идентификатор населенного пункта")]
		public Guid? NearestSettlementId { get; set; }

		[DisplayName("Идентификатор муниципального района")]
		public Guid? MunicipalDistrictId { get; set; }

		[DisplayName("Идентификатор уязвимого объекта")]
		public Guid? NearestVulnerableObjectId { get; set; }

		[DisplayName("Идентификатор пожара")]
		public Guid? FireId { get; set; }

		[DisplayName("Идентификатор внедрения")]
		public Guid? ProductionId { get; set; }

		[DisplayName("Идентификатор типа оповещения")]
		public byte ThreatTypeId { get; set; }

		[DisplayName("Идентификатор критерия")]
		public byte ThreatCriteriaId { get; set; }

		public bool IsDeleted { get; set; }
		
		/// <summary>
		/// Дата наступления угрозы
		/// </summary>
		[DisplayName("Дата наступления угрозы")]
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Дата ликвидации угрозы
		/// </summary>
		[DisplayName("Дата ликвидации угрозы")]
		public DateTime? EndDate { get; set; }

		[ForeignKey("NearestSettlementId")]
		[DisplayName("ближайший нас. пункт")]
		public virtual SettlementOktmo? NearestSettlement { get; set; }

		[ForeignKey("MunicipalDistrictId")]
		[DisplayName("Муниципальный район")]
		public virtual SettlementOktmo? MunicipalDistrict { get; set; }

		[ForeignKey("NearestVulnerableObjectId")]
		[DisplayName("ближайший уязвимый объект")]
		public virtual VulnerableObject? NearestVulnerableObject { get; set; }

		[ForeignKey("FireId")]
		[DisplayName("Пожар")]
		public virtual Fire? Fire { get; set; }
	}
}
