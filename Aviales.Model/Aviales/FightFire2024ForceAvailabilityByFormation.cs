using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	[Table("FightFire2024ForceAvailabilityByFormation", Schema = "Aviales")]
	[DisplayName("Наличие сил и средств тушения по формированию")]
	[Serializable]
	[AssociativeToString("Наличие сил и средств тушения: {FightFireForceKind}")]
	public partial class FightFire2024ForceAvailabilityByFormation : AuditModelBase
	{
		[Key]
        [DisplayName("Идентификатор записи")]
		public Guid FightFireForceAvailabilityByFormationsId {get; set;}

        [DisplayName("Идентификатор отчета")]
        public Guid FightFireForceAvailabilityByFormationsReportId {get; set;}
        [DisplayName("Идентификатор формирования")]
		public Guid? FightFireFormationKindId {get; set; }

        [DisplayName("Идентификатор СиС")]
        public Guid FightFireForceKindId {get; set; }

        [DisplayName("Лес фонд")]
        public int? ForestFond { get; set; }

        [DisplayName("Мин Обороны")]
		public int? MinistryDefense { get; set;}

        [DisplayName("Особо охраняемые природные территории")]
		public int? SpeciallyProtectedAreas { get; set; }

        [DisplayName("Иные территроии")]
        public int? OtherCategories { get; set; }

        [DisplayName("Населенный пункт")]
        public int? InhabitedLocality { get; set; }

        [ForeignKey("FightFireForceKindId")]
		public virtual FightFireForceKind? FightFireForceKind {get; set;}

		[ForeignKey("FightFireFormationKindId")]
		public virtual FightFireFormationKind? FightFireFormationKind {get; set;}

		[ForeignKey("FightFireForceAvailabilityByFormationsReportId")]
		public virtual FightFire2024ForceAvailabilityByFormationReport? FightFireForceAvailabilityByFormationsReports {get; set;}

        public override string ToAuditString()
        {
            return "ReportItem";//TODO сделать красиво
        }
    }
}