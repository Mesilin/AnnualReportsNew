using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{
    [Table("FightLandscapeFireTeam", Schema = "LandscapeFire")]
    [DisplayName("Силы задействованные на ландшафтном пожаре")]
    [Serializable]
    [AssociativeToString("{FightFireTeamLocal}")]
public partial class FightLandscapeFireTeam : AuditModelBase
    {
        /// <summary>
        /// Идентификатор сил на тушении
        /// </summary>
        [Key]
        [DisplayName("Идентификатор сил на тушении")]
        public Guid FightLandscapeFireTeamId { get; set; }

        /// <summary>
        /// Количество доставленных сил
        /// </summary>
        [DisplayName("Количество доставленных сил")]
        public short? Delivered
        {
            get { return delivered; }
            set
            {
                delivered = value;
                OnPropertyChanged();
            }
        }
        private short? delivered;
        
        /// <summary>
        /// Количество работающих сил
        /// </summary>
        [DisplayName("Количество работающих сил")]
        public short? Working
        {
            get { return working; }
            set
            {
                working = value;
                OnPropertyChanged();
            }
        }
        private short? working;

        /// <summary>
        /// Количество дополнительно требующихся сил
        /// </summary>
        [DisplayName("Количество дополнительно требующихся сил")]
        public short? Required { get; set; }

        /// <summary>
        /// Количество вывезенных сил
        /// </summary>
        [DisplayName("Количество вывезенных сил")]
        public short? Exported
        {
            get { return exported; }
            set
            {
                exported = value;
                OnPropertyChanged();
            }
        }
        private short? exported;

        /// <summary>
        /// Количество сил, доставленных авиацией
        /// </summary>
        [DisplayName("Количество сил, доставленных авиацией")]
        public short? DeliveredAir { get; set; }

        /// <summary>
        /// Силы являются работниками авиапожарных команд
        /// </summary>
        [DisplayName("Силы являются работниками авиапожарных команд")]
        public bool ByAviabase { get; set; }

        /// <summary>
        /// Количество сил, которое планируется привлечь к тушению
        /// </summary>
        [DisplayName("Количество сил, которое планируется привлечь к тушению")]
        public short? Planned { get; set; }

        /// <summary>
        /// Идентификатор динамики
        /// </summary>
        [DisplayName("Идентификатор динамики")]
        public Guid LandscapeFireDynamicId { get; set; }

        /// <summary>
        /// Идентификатор типа команд, участвующих при тушении
        /// </summary>
        [DisplayName("Идентификатор типа команд, участвующих при тушении")]
        public Guid FightFireTeamLocalId
        {
            get { return fightFireTeamLocalId; }
            set
            {
                fightFireTeamLocalId = value;
                OnPropertyChanged();
            }
        }
        private Guid fightFireTeamLocalId;

        /// <summary>
        /// Транспорт доставки
        /// </summary>
        [DisplayName("Транспорт доставки")]
        public Guid? TransportId
        {
            get { return transportId; }
            set
            {
                transportId = value;
                OnPropertyChanged();
            }
        }
        private Guid? transportId;

        /// <summary>
        /// Идентификатор лесничества, которому принадлежит команда
        /// </summary>
        [DisplayName("Лесничество, которому принадлежит команда")]
        public Guid? ForestryId
        {
            get { return forestryId; }
            set
            {
                forestryId = value;
                OnPropertyChanged();
            }
        }
        private Guid? forestryId;

        /// <summary>
        /// Тип команды
        /// </summary>
        [ForeignKey("FightFireTeamLocalId")]
        [DisplayName("Тип команды")]
        public virtual FightFireTeamLocal FightFireTeamLocal { get; set; } = null!;

        /// <summary>
        /// Динамика
        /// </summary>
        [ForeignKey("LandscapeFireDynamicId")]
        [DisplayName("Динамика")]
        public virtual LandscapeFireDynamic? LandscapeFireDynamic { get; set; }

        /// <summary>
        /// Транспорт
        /// </summary>
        [ForeignKey("TransportId")]
        [DisplayName("Транспорт")]
        public virtual Transport? Transport { get; set; }

        /// <summary>
        /// Лесничество
        /// </summary>
        [ForeignKey("ForestryId")]
        [DisplayName("Лесничество")]
        public virtual Forestry? Forestry { get; set; }

        public override string ToAuditString()
        {
            return "Задействованные силы";
        }
    }
}