using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Flight
{

    [Table("FireFlight", Schema = "Flight")]
    [DisplayName("Налёт")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Налет от: {FlightStartDate}")]
	public partial class FireFlight : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireFlightId { get; set; }

        [DisplayName("№ заявки")]
        public string? RequestNumber { get; set; }

        [DisplayName("Дата заявки")]
        public DateTime? RequestDate { get; set; }

        [DisplayName("Дата вылета")]
        public DateTime FlightStartDate { get; set; }

        [DisplayName("Дата прилета")]
        public DateTime? FlightEndDate { get; set; }
        
        [DisplayName("Идентификатор ВС")]
        public Guid FireFlightAircraftId { get; set; }

        [DisplayName("Идентификатор авиаотделения")]
        public Guid? AirbaseDepartmentId { get; set; }

        [ForeignKey("AirbaseDepartmentId")]
        [DisplayName("Авиаотделение")]
        public virtual AirbaseDepartment? AirbaseDepartment { get; set; }

        [ForeignKey("FireFlightAircraftId")]
        [DisplayName("ВС")]
        public virtual FireFlightAircraft FireFlightAircraft { get; set; } = null!;

		[MaxLength(100)]
        [DisplayName("Номер ВС")]
        public string? FireFlightAircraftNumber { get; set; }

        [DisplayName("Осмотренные авиаотделения")]
        [Aggregation]
        public virtual ICollection<FireFlightInspection> FireFlightInspections { get; set; } = new List<FireFlightInspection>();

        [DisplayName("Группы оплаты")]
        [Aggregation]
        public virtual ICollection<FireFlightPaymentGroup> FireFlightPaymentGroups { get; set; } = new List<FireFlightPaymentGroup>();

        [DisplayName("Личный состав налета")]
        [Aggregation]
        public virtual ICollection<FireFlightPersonnel> FireFlightPersonnels { get; set; } = new List<FireFlightPersonnel>();

        [DisplayName("Выполненные работы")]
        [Aggregation]
        public virtual ICollection<FireFlightWork> FireFlightWorks { get; set; } = new List<FireFlightWork>();

        [DisplayName("IsPlan")]
        public bool IsPlan { get; set; }

        [DisplayName("Идентификатор плана налета")]
        public Guid? PlanId { get; set; }

        [ForeignKey("PlanId")]
        //[DisplayName("План налёта")]
        public virtual FireFlight? Plan { get; set; }

        [NotMapped]
        public string? FireFlightName
        {
            get
            {
                string time = string.Empty;
                if (FlightStartDate != DateTime.MinValue)
                    time = $"{FlightStartDate:dd.MM.yyyy HH:mm}";
                return $"{time} {AirbaseDepartment?.ShortName}".TrimEnd();
            }
        }

        [MaxLength(500)]
        [DisplayName("Примечание")]
        public string? Description { get; set; }


        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        public override string ToAuditString()
        {
            string time = string.Empty;
            if (FlightStartDate != DateTime.MinValue)
                time = string.Format("{0:dd.MM.yyyy HH:mm}", FlightStartDate);
            return string.Format("{0} {1} {2}",IsPlan?"План налета от ":"Налет от ", time, AirbaseDepartment != null ? AirbaseDepartment.ShortName : null).TrimEnd();
        }
    }
}