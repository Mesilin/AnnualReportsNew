using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	[Table("FightFireForceAvailabilityByFormation", Schema = "Aviales")]
	[DisplayName("Наличие сил и средств тушения по формированию")]
	[Serializable]
	[AssociativeToString("Наличие сил и средств тушения: {FightFireForceKind}")]
	public partial class FightFireForceAvailabilityByFormation : AuditModelBase
	{
		[Key]
        [DisplayName("Идентификатор записи")]
		public Guid FightFireForceAvailabilityByFormationId {get; set;}

        [DisplayName("Идентификатор отчета")]
        public Guid FightFireForceAvailabilityByFormationReportId {get; set;}
        [DisplayName("Идентификатор формирования")]
		public Guid? FightFireFormationKindId {get; set; }

        [DisplayName("Идентификатор СиС")]
        public Guid FightFireForceKindId {get; set; }

		/// <summary>
		/// Готовые
		/// </summary>
        [DisplayName("Количество готовых")]
        public int? ReadyCount {get; set; }

		/// <summary>
		/// Занятые
		/// </summary>
        [DisplayName("Количество занятых")]
		public int? WorkingCount {get; set;}

		/// <summary>
		/// Наличие
		/// </summary>
        [DisplayName("Наличие")]
		public int? AvailabilityCount { get; set; }

		[ForeignKey("FightFireForceKindId")]
		public virtual FightFireForceKind FightFireForceKind {get; set;}

		[ForeignKey("FightFireFormationKindId")]
		public virtual FightFireFormationKind? FightFireFormationKind {get; set;}

		[ForeignKey("FightFireForceAvailabilityByFormationReportId")]
		public virtual FightFireForceAvailabilityByFormationReport FightFireForceAvailabilityByFormationReport {get; set;}

        public override string ToAuditString()
        {
            return "ReportItem";//TODO сделать красиво
        }
    }
}