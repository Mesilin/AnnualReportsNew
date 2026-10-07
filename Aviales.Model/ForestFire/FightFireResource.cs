using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
    [Table("FightFireResource", Schema = "ForestFire")]
	[DisplayName("Технические средства задействованные на пожаре")]
	[Serializable]
    [AssociativeToString("{FightFireResourceLocal}")]
	public class FightFireResource : FightFireResourceParameters
	{
        [Key]
        [DisplayName("Идентификатор технического средства")]
        public Guid FightFireResourceId { get; set; }

        [DisplayName("Идентификатор динамики")]
        public Guid FireDynamicId { get; set; }

        [ForeignKey("FireDynamicId")]
        public virtual FireDynamic? FireDynamic { get; set; }

        public override string ToAuditString()
        {
            return "Средства";
        }
    }
}