using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{
    /// <summary>
    /// Выполненная работа
    /// </summary>
    [Table("FireFlightWork", Schema = "Flight")]
    [DisplayName("Выполненная работа")]
    [Serializable]
	[AssociativeToString("Выполненная работа в ходе: {FireFlight}")]
	public class FireFlightWork : FireFlightWorkParameters
    {
        [Key]
        [DisplayName("Идентификатор выполненной работы")]
        public Guid FireFlightWorkId { get; set; }

        [DisplayName("Пожар")]
        public Guid? FireId { get; set; }

        [ForeignKey("FireId")]
        public virtual Fire? Fire { get; set; }

        public override string ToAuditString()
        {
            return "Выполненная работа в ходе налета";
        }
    }
}