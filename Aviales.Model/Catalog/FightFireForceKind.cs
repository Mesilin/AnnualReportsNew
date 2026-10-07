using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FightFireForceKind", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Типы сил и средств пожаротушения")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireForceKind : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FightFireForceKindId { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год справочника")]
        public int Year { get; set; }
        [DisplayName("Активность")]
        public bool IsActive { get; set; }
        [DisplayName("Начилие сил и средств тушения по формированию")]
        public virtual ICollection<FightFireForceAvailabilityByFormation> FightFireForceAvailabilityByFormations { get; set; } = new List<FightFireForceAvailabilityByFormation>();
        [DisplayName("Типы команд")]
        public virtual ICollection<FightFireTeamLocal> FightFireTeamLocals { get; set; } = new List<FightFireTeamLocal>();
        [DisplayName("Типы средств тушения")]
        public virtual ICollection<FightFireResourceLocal> FightFireResourceLocals { get; set; } = new List<FightFireResourceLocal>();

        public override string ToAuditString()
        {
            return Name;
        }
    }
}