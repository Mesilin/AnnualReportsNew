using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("FireHazardConsequence", Schema = "Aviales")]
    [DisplayName("ЧС. Потери")]
    [Serializable]
	[AssociativeToString("Потери при: {FireHazard}")]
	public partial class FireHazardConsequence : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireHazardConsequenceId { get; set; }

        [DisplayName("Идентификатор")]
        public Guid MunicipalDistrictId { get; set; }

        public Guid FireHazardId { get; set; }

        [DisplayName("КПО")]
        public int? Kpo { get; set; }

        [DisplayName("Погибших")]
        public int? DeadCount { get; set; }

        [DisplayName("Пострадавших")]
        public int? VictimCount { get; set; }

        [DisplayName("Пострадало объектов")]
        public int? DestructionCount { get; set; }

        [DisplayName("Примечание")]
        public string? Description { get; set; }

        [ForeignKey("MunicipalDistrictId")]
        [DisplayName("Муниципальный район")]
        public virtual SettlementOktmo MunicipalDistrict { get; set; } = null!;

        [ForeignKey("FireHazardId")]
        public virtual FireHazard FireHazard { get; set; } = null!;
    }
}