using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{

    [Table("FireFlightAircraft", Schema = "Flight")]
    [DisplayName("Воздушное судно из контракта")]
    [Serializable]
	[AssociativeToString("Воздушное судно \u2116 {Number}")]
	public partial class FireFlightAircraft : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireFlightAircraftId { get; set; }

        [DisplayName("Ведомственное")]
        public bool IsDepartmental { get; set; }

        [MaxLength(100)]
        [DisplayName("Бортовой №")]
        public string? Number { get; set; }

        //public bool IsActive {get; set;}
        [DisplayName("Идентификатор договора")]
        public Guid AircraftContractId { get; set; }

        [DisplayName("Идентификатор модели")]
        public Guid AircraftModelId { get; set; }

        [DisplayName("Лимит летного времени, мин")]
        public int? FlightTime { get; set; }

        [DisplayName("Стоимость 1 часа")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Количество готовых ВС
        /// </summary>
        [DisplayName("Количество готовых ВС")]
        public int? ReadyCount { get; set; }

        [ForeignKey("AircraftContractId")]
        [DisplayName("Договор")]
        public virtual AircraftContract AircraftContract { get; set; } = null!;

		[ForeignKey("AircraftModelId")]
        [DisplayName("Модель")]
        public virtual AircraftModel AircraftModel { get; set; } = null!;

		[DisplayName("Список налетов")]
        public virtual ICollection<FireFlight> FireFlights { get; set; } = new List<FireFlight>();

        public override string ToAuditString()
        {
            return "Воздушное судно № " + Number;
        }
    }
}