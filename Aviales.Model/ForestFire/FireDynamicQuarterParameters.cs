using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
    public abstract class FireDynamicQuarterParameters : AuditModelBase
    {
        [DisplayName("Идентификатор урочища")]
        public Guid? ForestryTractId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Список кварталов")]
        public string? Quarter { get; set; }

        [MaxLength(200)]
        [DisplayName("Список выделов")]
        public string? Stratum { get; set; }

        [ForeignKey("ForestryTractId")]
        public virtual ForestryTract? ForestryTract { get; set; }
    }
}
