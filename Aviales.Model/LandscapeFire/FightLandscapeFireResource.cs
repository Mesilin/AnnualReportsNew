using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{

    [Table("FightLandscapeFireResource", Schema = "LandscapeFire")]
    [DisplayName("Технические средства задействованные на ландшафтном пожаре")]
    [Serializable]
	[AssociativeToString("{FightFireResourceLocal}")]
	public partial class FightLandscapeFireResource : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор технического средства")]
        public Guid FightLandscapeFireResourceId { get; set; }

        [DisplayName("Количество применяемого технического средства")]
        public short? Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }
        private short? amount;

        [DisplayName("Количество требующегося технического средства")]
        public short? Required
        {
            get { return required; }
            set
            {
                required = value;
                OnPropertyChanged();
            }
        }
        public short? required;

        [DisplayName("Количество дополнительно требующегося технического средства")]
        public short? Planned
        {
            get { return planned; }
            set
            {
                planned = value;
                OnPropertyChanged();
            }

        }
        public short? planned;

        [DisplayName("Идентификатор типа технического средства")]
        public Guid FightFireResourceLocalId
        {
            get { return fightFireResourceLocalId; }
            set
            {
                fightFireResourceLocalId = value;
                OnPropertyChanged();
            }
        }
        private Guid fightFireResourceLocalId;

        [DisplayName("Идентификатор типа подразделения-владельца технического средства")]
        public Guid? FightFireTeamLocalId
        {
            get { return fightFireTeamLocalId; }
            set
            {
                fightFireTeamLocalId = value;
                OnPropertyChanged();
            }
        }
        private Guid? fightFireTeamLocalId;

        [DisplayName("Идентификатор динамики")]
        public Guid LandscapeFireDynamicId { get; set; }

        [ForeignKey("FightFireResourceLocalId")]
        [DisplayName("Тип средств тушения")]
        public virtual FightFireResourceLocal FightFireResourceLocal { get; set; } = null!;

        [ForeignKey("FightFireTeamLocalId")]
        [DisplayName("Тип команды")]
        public virtual FightFireTeamLocal? FightFireTeamLocal { get; set; }

        [ForeignKey("LandscapeFireDynamicId")]
        [DisplayName("Динамика")]
        public virtual LandscapeFireDynamic? LandscapeFireDynamic { get; set; }

        public override string ToAuditString()
        {
            return "Средства";
        }
    }
}