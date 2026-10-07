using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
	[Table("FightFireTeam", Schema = "ForestFire")]
	[DisplayName("Силы задействованные на пожаре")]
	[Serializable]
	[AssociativeToString("{FightFireTeamLocal}")]
	public class FightFireTeam : FightFireTeamParameters
	{
        [Key]
        [DisplayName("Идентификатор сил на тушении")]
        public Guid FightFireTeamId { get; set; }

        [DisplayName("Идентификатор динамики")]
        public Guid FireDynamicId { get; set; }

        [ForeignKey("FireDynamicId")]
        [DisplayName("Динамика пожара")]
        public virtual FireDynamic? FireDynamic { get; set; }

        public override string ToAuditString()
        {
            return "Силы задействованные на пожаре";
        }
    }
}