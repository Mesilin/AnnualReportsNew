using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	/// <summary>
	/// Родительская таблица объединяющая сущности для формы 2 ЛО:
	/// Противопожарные мероприятия (пункт 1) <see cref="FirePrevention"/>
	/// Инициирование дел об административных правонарушениях и уголовных дел (пункт 2) <see cref="FireSafetyRegulationViolation"/>
	/// </summary>
	[Table("FirePreventionAndSafetyRegulationViolation", Schema = "Aviales")]
	[DisplayName("Отчет о выполнении противопожарных мероприятий и инициировании дел об административных правонарушениях и уголовных дел")]
	[Serializable]
	[AssociativeToString("Отчет о выполнении противопожарных мероприятий и привлечении к ответственности за лесные пожары за {FirePreventionAndSafetyRegulationViolationDate}")]
	public partial class FirePreventionAndSafetyRegulationViolation : AuditModelBase
	{
		[Key]
        [DisplayName("Идентификатор отчета")]
        public Guid FirePreventionAndSafetyRegulationViolationId {get; set;}

		/// <summary>
		/// Дата
		/// </summary>
		[DisplayName("Дата отчета")]
		public DateTime FirePreventionAndSafetyRegulationViolationDate {get; set;}

		/// <summary>
		/// Идентификатор региона
		/// </summary>
		[DisplayName("Идентификатор субъекта")]
		public Guid RegionId { get; set;}

        [ForeignKey("RegionId")]
		[DisplayName("Субъект")]
		public virtual Region Region {get; set;}

		[Aggregation]
		[DisplayName("Противопожарные мероприятия")]
		public virtual FirePrevention FirePrevention { get; set; } = null!;

		[Aggregation]
		[DisplayName("Инициирование дел")]
		public virtual FireSafetyRegulationViolation FireSafetyRegulationViolation { get; set; } = null!;

        public override string ToAuditString()
        {
            return
                "Отчет о выполнении противопожарных мероприятий и привлечении к ответственности за лесные пожары за " +
                FirePreventionAndSafetyRegulationViolationDate.ToShortDateString();
        }
    }
}
