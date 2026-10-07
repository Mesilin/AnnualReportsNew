using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Aviales
{

    [Table("FireHazardAct", Schema = "Aviales")]
    [DisplayName("Акт пожароопасности")]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Акт пожароопасности от {ActDate}")]
	public partial class FireHazardAct : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireHazardActId { get; set; }
        
        [Required]
        [DisplayName("Дата")]
        public DateTime ActDate { get; set; }

        [MaxLength(2000)]
        [DisplayName("Основание и номер")]
        public string? NameAndNumber { get; set; }

        [MaxLength(2000)]
        [DisplayName("Примечание")]
        public string? Note { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }
        
        [DisplayName("Режимы по районам")]
        [Aggregation]
        public virtual ICollection<FireHazardActRow> FireHazardActRows { get; set; } = new List<FireHazardActRow>();

        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return $"Акт пожароопасности от {ActDate.ToShortDateString()}";
        }
    }
}