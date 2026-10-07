using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Flight
{

    [Table("AircraftContract", Schema = "Flight")]
    [DisplayName("Договор на воздушное судно")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Договор \u2116 {Number}")]
	public partial class AircraftContract : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор контракта")]
        public Guid AircraftContractId { get; set; }

        [DisplayName("Идентификатор подрядчика")]
        public Guid ContractorId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Номер договора")]
        public string? Number { get; set; }
        
        [DisplayName("Дата начала контракта")]
        public DateTime AircraftContractDate { get; set; }

        [DisplayName("Дата окончания контракта")]
        public DateTime AircraftContractEndDate { get; set; }

        [DisplayName("Комментарий")]
        public string? Comment { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }

        [DisplayName("Общий лимит лётного времени, мин")]
        public int? TotalFlightTime { get; set; }

        [ForeignKey("ContractorId")]
        [DisplayName("Подрядчик")]
        public virtual Contractor Contractor { get; set; } = null!;

        [DisplayName("Список ВС")]
        [Aggregation]
        public virtual ICollection<FireFlightAircraft> FireFlightAircrafts { get; set; } = new List<FireFlightAircraft>();

        [DisplayName("Стоимость работ")]
        [Aggregation]
        public virtual ICollection<FireFlightWorkCost> FireFlightWorkCosts { get; set; } = new List<FireFlightWorkCost>();

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Внедрение")]
        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return "Договор №" + Number + "(" + AircraftContractDate.ToShortDateString() + "-" +
                   AircraftContractEndDate.ToShortDateString() + ")";
        }
    }
}