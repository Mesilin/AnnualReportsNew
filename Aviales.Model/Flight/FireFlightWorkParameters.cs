using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Serializable]
    public abstract class FireFlightWorkParameters : AuditModelBase
    {
        [DisplayName("Время, минут")]
        public int WorkTime { get; set; }

        [DisplayName("Номер маршрута патрулирования")]
        [MaxLength(30)]
        public string? PatrolRouteNumber { get; set; }

        [DisplayName("Сливы")]
        public int? PourCount { get; set; }

        [DisplayName("Объём Сливов")]
        public decimal? PourVolume { get; set; }

        [DisplayName("Воздействия")]
        public int? InfluenceCount { get; set; }

        [DisplayName("Прыжки")]
        public int? Jump { get; set; }

        [DisplayName("Спуски")]
        public int? Descent { get; set; }

        [DisplayName("Налёта")]
        public Guid FireFlightId { get; set; }

        [DisplayName("Тип работ")]
        public Guid FireFlightWorkKindId { get; set; }

        [DisplayName("Принадлежность")]
        public Guid ForestOwnerKindId { get; set; }

        [ForeignKey("FireFlightWorkKindId")]
        public virtual FireFlightWorkKind FireFlightWorkKind { get; set; } = null!;

		[ForeignKey("ForestOwnerKindId")]
        public virtual ForestOwnerKind? ForestOwnerKind { get; set; }

        [ForeignKey("FireFlightId")]
        public virtual FireFlight? FireFlight { get; set; }

        [DisplayName("Группы оплаты работ")]
        public virtual ICollection<FireFlightPaymentGroup> FireFlightPaymentGroups { get; set; } = new List<FireFlightPaymentGroup>();
    }
}
