using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{

    [Table("FireFlightPersonnel", Schema = "Flight")]
    [DisplayName("Личный состав налета")]
    [Serializable]
	[AssociativeToString("Л/С налета {Personnel}")]
	public partial class FireFlightPersonnel : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор записи")]
        public Guid FireFlightPersonnelId { get; set; }
        
        [DisplayName("Проверяющий")]
        public bool? IsControl { get; set; }
        [DisplayName("Стажер")]
        public bool? IsTrainee { get; set; }
        [DisplayName("Идентификатор налета")]
        public Guid FireFlightId { get; set; }
        [DisplayName("Идентификатор летчика-наблюдателя")]
        public Guid PersonnelId { get; set; }

        [ForeignKey("PersonnelId")]
        public virtual Personnel Personnel { get; set; } = null!;

		[ForeignKey("FireFlightId")]
        public virtual FireFlight FireFlight { get; set; } = null!;

		public override string ToAuditString()
        {
            return "Личный состав";
        }
    }
}