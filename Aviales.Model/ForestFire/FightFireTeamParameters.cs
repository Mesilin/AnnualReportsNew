using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
	public abstract class FightFireTeamParameters : AuditModelBase
    {
        [DisplayName("Количество доставленных сил")]
		public short? Delivered { get; set; }

		[DisplayName("Количество работающих сил")]
		public short? Working { get; set; }

		[DisplayName("Количество дополнительно требующихся сил")]
		public short? Required { get; set; }

		[DisplayName("Количество вывезенных сил")]
		public short? Exported { get; set; }

		[DisplayName("Количество сил, доставленных авиацией")]
		public short? DeliveredAir { get; set; }

		[DisplayName("Силы являются работниками авиапожарных команд")]
		public bool ByAviabase { get; set; }

		[DisplayName("Количество сил, которое планируется привлечь к тушению")]
		public short? Planned { get; set; }
		
		[DisplayName("Идентификатор типа команд, участвующих при тушении")]
		public Guid FightFireTeamLocalId { get; set; }

		[DisplayName("Транспорт доставки")]
		public Guid? TransportId { get; set; }

		[DisplayName("Лесничество, которому принадлежит команда")]
		public Guid? ForestryId { get; set; }

		[ForeignKey("FightFireTeamLocalId")]
		[DisplayName("Тип команды")]
		public virtual FightFireTeamLocal FightFireTeamLocal { get; set; } = null!;
		
		[ForeignKey("TransportId")]
		[DisplayName("Транспорт")]
		public virtual Transport? Transport { get; set; }

		[ForeignKey("ForestryId")]
		[DisplayName("Лесничество, которому принадлежит команда")]
		public virtual Forestry? Forestry { get; set; }
	}
}
