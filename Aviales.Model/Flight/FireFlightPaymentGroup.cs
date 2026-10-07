using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{

    [Table("FireFlightPaymentGroup", Schema = "Flight")]
    [DisplayName("Группы оплаты работ по налетам")]
    [Serializable]
	[AssociativeToString("Группа оплаты {FireFlightPersonnel}")]
	public partial class FireFlightPaymentGroup : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор группы работ")]
        public Guid FireFlightPaymentGroupId { get; set; }

        [DisplayName("Время налёта по группе 1")]
        public int? TimeByRate1 { get; set; }

        [DisplayName("Время налёта по группе 2")]
        public int? TimeByRate2 { get; set; }

        [DisplayName("Время налёта по группе 3")]
        public int? TimeByRate3 { get; set; }

        [DisplayName("Время налёта по группе 4")]
        public int? TimeByRate4 { get; set; }

        [DisplayName("Время налёта по группе 5")]
        public int? TimeByRate5 { get; set; }

        [DisplayName("Время налёта по группе 6")]
        public int? TimeByRate6 { get; set; }

        [DisplayName("Вознаграждение")]
        public decimal? Payment { get; set; }

        [DisplayName("Идентификатор выполненной работы")]
        public Guid FireFlightWorkId { get; set; }

        [DisplayName("Идентификатор налёта по людям")]
        public Guid FireFlightPersonnelId { get; set; }

        [DisplayName("Идентификатор налёта")]
        public Guid FireFlightId { get; set; }

        [ForeignKey("FireFlightId")]
        public virtual FireFlight FireFlight { get; set; } = null!;

		[ForeignKey("FireFlightPersonnelId")]
        public virtual FireFlightPersonnel FireFlightPersonnel { get; set; } = null!;

		[ForeignKey("FireFlightWorkId")]
        public virtual FireFlightWork FireFlightWork { get; set; } = null!;

		[DisplayName("Учитывать в ведомости оплаты")]
        public bool IsAccountInPayroll { get; set; }

        public override string ToAuditString()
        {
            return "Группа оплаты работ по налетам";
        }
    }
}