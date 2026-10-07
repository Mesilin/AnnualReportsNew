using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
    public abstract class FightFireResourceParameters : AuditModelBase
    {
        [DisplayName("Количество применяемого технического средства")]
        public short? Amount { get; set; }

        [DisplayName("Количество требующегося технического средства")]
        public short? Required { get; set; }

        [DisplayName("Количество дополнительно требующегося технического средства")]
        public short? Planned { get; set; }

        [DisplayName("Идентификатор типа технического средства")]
        public Guid FightFireResourceLocalId { get; set; }

        [DisplayName("Идентификатор типа подразделения-владельца технического средства")]
        public Guid? FightFireTeamLocalId { get; set; }

        [ForeignKey("FightFireResourceLocalId")]
        public virtual FightFireResourceLocal FightFireResourceLocal { get; set; } = null!;

        [ForeignKey("FightFireTeamLocalId")]
        public virtual FightFireTeamLocal? FightFireTeamLocal { get; set; }
    }
}
