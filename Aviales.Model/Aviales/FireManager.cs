using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;
using UIP.Core.Model.Security;

namespace Aviales.Model.Aviales
{
    [Table("FireManager", Schema = "Aviales")]
    [Serializable]
    [AssociativeToString("{Person}")]
	public class FireManager : AuditModelBase, IProductionDependent
    {
        [Key]
        
        [DisplayName("Идентификатор")]
        public Guid FireManagerId { get; set; }

        [DisplayName("Идентификатор Человека")]
        public Guid PersonId { get; set; }

        [DisplayName("Наименование Лесничества")]
        public string? ForestryName { get; set; }

        [Required(ErrorMessage = "ForestryCode is Required")]
        [DisplayName("Код Лесничества")]
        public string? ForestryCode { get; set; }

        [DisplayName("Код Принадлежности")]
        public decimal ForestOwnerKindCode { get; set; }

        [DisplayName("Опыт работы")]
        public int? Experience { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; } = null!;

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }
    }
}
