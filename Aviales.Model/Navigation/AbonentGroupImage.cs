using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Navigation
{
    [Table("AbonentGroupImage", Schema = "Navigation")]
    [DisplayName("Изображение группы абонентов")]
    public class AbonentGroupImage : ModelBase
    {
        [Key]
        [DisplayName("Уникальный идентификатор")]
        public Guid AbonentGroupImageId { get; set; }

        [DisplayName("Бинарные данные")]
        public byte[] Data { get; set; }

        [DisplayName("Время создания")]
        public DateTime? CreateDateTime { get; set; }

    }
}
