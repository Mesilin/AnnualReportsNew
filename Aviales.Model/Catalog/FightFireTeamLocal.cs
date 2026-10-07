using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{

    [Table("FightFireTeamLocal", Schema = "Catalog")]
    [DisplayName("Региональные справочники. Типы команд")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireTeamLocal : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FightFireTeamLocalId { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Краткое Наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год справочника")]
        public int Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Идентификатор типа сил и средств пожаротушения")]
        public Guid? FightFireForceKindId { get; set; }

        [DisplayName("Идентификатор типа ЛПФ")]
        public Guid FightFireFormationKindId { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [ForeignKey("FightFireForceKindId")]
        [DisplayName("Тип сил и средств пожаротушения")]
        public virtual FightFireForceKind? FightFireForceKind { get; set; }

        [ForeignKey("FightFireFormationKindId")]
        [DisplayName("Тип ЛПФ")]
        public virtual FightFireFormationKind FightFireFormationKind { get; set; }

        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}