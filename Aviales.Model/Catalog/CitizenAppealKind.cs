using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{

    [Table("CitizenAppealKind", Schema = "Catalog")]
    [DisplayName("Тип обращения граждан")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("{Name}")]
	public partial class CitizenAppealKind : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid CitizenAppealKindId { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }
        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Список обращений")]
        public virtual ICollection<CitizenAppeal> CitizenAppeals { get; set; } = new List<CitizenAppeal>();

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }
        [ForeignKey("ProductionId")]

        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}