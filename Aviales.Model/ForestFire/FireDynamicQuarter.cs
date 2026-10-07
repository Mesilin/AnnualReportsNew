using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{

	[Table("FireDynamicQuarter", Schema = "ForestFire")]
	[DisplayName("Квартал / выдел")]
	[Serializable]
	[AssociativeToString("Квартал / выдел :{Quarter} / {Stratum}")]
	public class FireDynamicQuarter : FireDynamicQuarterParameters
    {
        [Key]
        [DisplayName("Идентификатор квартала / выдела")]
        public Guid FireDynamicQuarterId { get; set; }

        [DisplayName("Идентификатор динамики")]
        public Guid FireDynamicId { get; set; }

        [DisplayName("Динамика пожара")]
        [ForeignKey("FireDynamicId")]
        public virtual FireDynamic? FireDynamic { get; set; }

        public override string ToAuditString()
        {
            return "Список кварталов и выделов";
        }
    }
}