using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;


// TODO: Alexander Anokhin Evgeniy Mesilin::Содержимое и название таблицы не соответствуют друг другу, нужно переименовать таблицу:2024-08-15
namespace Aviales.Model.Input
{
    /// <summary>
	/// Поле содержит значение колонки "Примечание" с сайта ИСДМ и по сути своей им и является. Это не причины исключения как следует из названия.
	/// </summary>
	[Table("IsdmHotSpotExclusionReasonKind", Schema = "Input")]
    //[DisplayName("Вид причины исключения ИСДМ-точки")]
    [DisplayName("Примечание")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class IsdmHotSpotExclusionReasonKind : AuditModelBase
    {
        [Key]
        public System.Guid IsdmHotSpotExclusionReasonKindId { get; set; }

        public decimal Code { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool IsActive { get; set; }
    }
}