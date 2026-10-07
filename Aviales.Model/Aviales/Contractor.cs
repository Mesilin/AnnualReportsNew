using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Flight;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Aviales
{

    [Table("Contractor", Schema = "Aviales")]
    [DisplayName("Подрядчик")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("{Name}")]
	public partial class Contractor : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid ContractorId { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Является авиаподрядчиком?")]
        public bool? IsAviaContractor { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Договор")]
        public virtual ICollection<AircraftContract> AircraftContracts { get; set; } = new List<AircraftContract>();

        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}