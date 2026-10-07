using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Aviales.Model.Oktmo;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("ForestryDistrict", Schema = "Catalog")]
	[DisplayName("Участковое лесничество")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class ForestryDistrict : AuditModelBase//, IKindType2
	{
        [Key]
		[DisplayName("Идентификатор участкового лесничества")]
		public Guid ForestryDistrictId {get; set;}

        [DisplayName("Идентификатор лесничества")]
        public Guid ForestryId {get; set;}

		[Required]
		[MaxLength(3)]
        [IsBasic]
        [DisplayName("Код")]
        public string? Code {get; set;}
		[MaxLength(4)]

        [DisplayName("Код CodeLv")]
        [IsBasic]
        public string? CodeLv {get; set;}

		[Required]
        [DisplayName("Наименование")]
        [IsBasic]
        public string? Name {get; set;}

		[DisplayName("Площадь земель")]
        public decimal? InspectArea {get; set;}

		public Guid? ForestOwnerKindId {get; set;}
		public Guid? MonitoringZoneKindId {get; set;}
		public Guid? AirbaseDepartmentId {get; set;}
		public Guid? MunicipalDistrictIdOld {get; set;}
        public Guid? FightFireZoneId { get; set; }

        [DisplayName("Год справочника")]
        [IsBasic]
        public int Year {get; set;}

        [IsBasic]
        [DisplayName("Активность")]

		public bool IsActive {get; set;}
		public Guid? MunicipalDistrictId {get; set;}

		public Guid? ForestryDistrictGeometryId { get; set; }

		//[ForeignKey("ForestryDistrictGeometryId")]
  //      [DisplayName("Геометрия участкового лесничества")]
  //      public virtual ForestryDistrictGeometry? ForestryDistrictGeometry { get; set; }

        [DisplayName("Авиаотделение")]
		[ForeignKey("AirbaseDepartmentId")]
        public virtual AirbaseDepartment? AirbaseDepartment {get; set;}

        [DisplayName("Лесничество")]
        [ForeignKey("ForestryId")]
        public virtual Forestry Forestry { get; set; } = null!;

		//public virtual ICollection<FederalFire> FederalFires {get; set;}
		public virtual ICollection<Fire> Fires {get; set; } = new List<Fire>();

        [DisplayName("Принадлежность земель")]
		[ForeignKey("ForestOwnerKindId")]
        public virtual ForestOwnerKind? ForestOwnerKind {get; set;}

        [DisplayName("Зона мониторинга")]
		[ForeignKey("MonitoringZoneKindId")]
        public virtual MonitoringZoneKind? MonitoringZoneKind {get; set;}

        [DisplayName("Муниципальный район")]
		[ForeignKey("MunicipalDistrictId")]
        public virtual SettlementOktmo? MunicipalDistrict {get; set;}

        [DisplayName("Муниципальный район(архивное поле)")]
		[ForeignKey("MunicipalDistrictIdOld")]
        public virtual MunicipalDistrict? MunicipalDistrictOld {get; set;}

        [DisplayName("Районы применения сил и средств")]
        [ForeignKey("FightFireZoneId")]
        public virtual FightFireZone? FightFireZone { get; set; }

        [DisplayName("Список урочищ")]
        [Aggregation]
		public virtual ICollection<ForestryTract> ForestryTracts {get; set; } = new List<ForestryTract>();
//public virtual ICollection<PreventiveBurningDetailedPlan> PreventiveBurningDetailedPlans {get; set;}

        public override string ToAuditString()
        {
            return Name;
        }
    }
}