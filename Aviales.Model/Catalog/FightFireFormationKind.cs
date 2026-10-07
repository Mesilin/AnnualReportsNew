using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FightFireFormationKind", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Лесопожарные формирования")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireFormationKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public System.Guid FightFireFormationKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год справочника")]
        public int Year { get; set; }
        [DisplayName("Начилие сил и средств тушения по формированию")]
        public virtual ICollection<FightFireForceAvailabilityByFormation> FightFireForceAvailabilityByFormations { get; set; } = new List<FightFireForceAvailabilityByFormation>();
        [DisplayName("Типы команд")]
        public virtual ICollection<FightFireTeamLocal> FightFireTeamLocals { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}