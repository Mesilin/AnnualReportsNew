using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("AttractedAircraft", Schema = "Aviales")]
    [DisplayName("Привлечённые ВС")]
    [Serializable]
	[AssociativeToString("{Mark} - {Number}")]
	public class AttractedAircraft : AttractedAircraftParameters
    {
        [Key]
        [DisplayName("Идентификатор ВС")]
        public Guid AttractedAircraftId { get; set; }

        [DisplayName("Идентификатор динамики пожара")]
        public Guid FireDynamicId { get; set; }

        [ForeignKey("FireDynamicId")]
        [DisplayName("Динамика")]
        public virtual FireDynamic FireDynamic { get; set; }

        public override string ToAuditString()
        {
            return Mark + " - " + Number;
        }
    }
}