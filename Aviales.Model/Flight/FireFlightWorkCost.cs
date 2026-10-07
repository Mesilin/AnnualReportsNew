using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{
    /// <summary>
    /// Стоимость работ по налётам
    /// </summary>
    [Table("FireFlightWorkCost", Schema = "Flight")]
    [DisplayName("Стоимость работ по налётам")]
    [Serializable]
	[AssociativeToString("Стоимость работ: {AircraftContract}")]
	public partial class FireFlightWorkCost : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireFlightWorkCostId { get; set; }

        [DisplayName("Идентификатор вида работ")]
        public Guid FireFlightWorkKindId { get; set; }

        [DisplayName("Лимит летного времени, мин.")]
        public int? FlightTime { get; set; }

        [DisplayName("Стоимость работ")]
        public decimal? Price { get; set; }

        [DisplayName("Идентификатор договора")]
        public Guid AircraftContractId { get; set; }

        [DisplayName("Идентификатор модели ВС")]
        public Guid AircraftModelId { get; set; }

        [ForeignKey("AircraftContractId")]
        [DisplayName("Договор")]
        public virtual AircraftContract AircraftContract { get; set; } = null!;

		[ForeignKey("AircraftModelId")]
        [DisplayName("Модель ВС")]
        public virtual AircraftModel AircraftModel { get; set; } = null!;

		[ForeignKey("FireFlightWorkKindId")]
        [DisplayName("Вид работ")]
        public virtual FireFlightWorkKind FireFlightWorkKind { get; set; } = null!;

		public override string ToAuditString()
        {
            return "Стоимость работ";
        }
    }
}