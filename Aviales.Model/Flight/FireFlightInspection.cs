using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{
    [Table("FireFlightInspection", Schema = "Flight")]
    [DisplayName("Осмотренные авиаотделения")]
    [Serializable]
	[AssociativeToString("Инспекция по: {AirbaseDepartment}")]
	public partial class FireFlightInspection : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор инспекции налёта")]
        public Guid FireFlightInspectionId { get; set; }
 
        [DisplayName("фактическая кратность авиапатрулирования")]
        public decimal? FactMultiplicity { get; set; }
        
        [DisplayName("Идентификатор инспектируемого налета")]
        public Guid FireFlightId { get; set; }
        
        [DisplayName("Идентификатор авиаотделения")]
        public Guid AirbaseDepartmentId { get; set; }

        [ForeignKey("AirbaseDepartmentId")]
        public virtual AirbaseDepartment AirbaseDepartment { get; set; } = null!;

		[ForeignKey("FireFlightId")]
        [DisplayName("Налет")]
        public virtual FireFlight FireFlight { get; set; }

        public override string ToAuditString()
        {
            return "Инспекция";
        }
    }
}