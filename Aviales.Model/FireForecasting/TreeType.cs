using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.FireForecasting
{
    [Table("TreeType", Schema = "FireForecasting")]
    [DisplayName("Тип дерева")]
    [Serializable]
    public class TreeType : ModelBase
    {
        [Key]
        [DisplayName("Идентификатор типа дерева")]
        public Guid TreeTypeId { get; set; }

        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Сокращённое наименование")]
        public string? ShortName { get; set; }
    }
}
