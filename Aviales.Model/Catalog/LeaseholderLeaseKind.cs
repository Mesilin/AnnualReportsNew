using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Catalog
{

    [Table("LeaseholderLeaseKind", Schema = "Catalog")]
    [DisplayName("—в€зка арендодател€ и вида использовани€ лесов")]
    public partial class LeaseholderLeaseKind : ModelBase
    {
        [Key]
        public System.Guid LeaseholderLeaseKindId { get; set; }
        public System.Guid LeaseholderId { get; set; }
        public System.Guid LeaseKindId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        [MaxLength(200)]
        public string? Owner { get; set; }
        [ForeignKey("LeaseholderId")]
        public virtual Leaseholder Leaseholder { get; set; }
        [ForeignKey("LeaseKindId")]
        public virtual LeaseKind LeaseKind { get; set; }
    }
}
