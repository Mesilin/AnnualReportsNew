using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Table("FlightLogWorkPlan", Schema = "Flight")]
    [DisplayName("Планируемый журнал полёта")]
    [Serializable]
    public class FlightLogWorkPlan : ModelBase
    {
        [Key]
        public Guid FlightLogWorkPlanId { get; set; }
        [MaxLength(500)]
        public string? PlanText { get; set; }
        public short? Number { get; set; }
        public Guid FlightLogId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime? Created { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public System.DateTime? Modified { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        [MaxLength(200)]
        public string? Owner { get; set; }

        [ForeignKey("FlightLogId")]
        public FlightLog FlightLog { get; set; }
    }
}
