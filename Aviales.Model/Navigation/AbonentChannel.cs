using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Navigation
{

    [Table("AbonentChannel", Schema = "Navigation")]
    [DisplayName("Канал абонентов")]
	[AssociativeToString("{Abonent}")]
	public partial class AbonentChannel : AuditModelBase
    {
        [DisplayName("Идентификатор канала абонентов")]
        [Key]
        public Guid AbonentChannelId { get; set; }

        [DisplayName("Идентификатор абонента")]
        public Guid AbonentId { get; set; }

        [Required]
        [DisplayName("Адрес")]
        [MaxLength(50)]
        public string? Address { get; set; }

        [DisplayName("Параметры")]
        [Required]
        public string? Parameters { get; set; }

        [DisplayName("Абонент")]
        [ForeignKey("AbonentId")]
        public virtual Abonent Abonent { get; set; } = null!;
    }
}