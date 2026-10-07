using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{
    [DisplayName("Тип важных объектов")]
    [Table("ImportantObjectType", Schema = "Catalog")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public class ImportantObjectType : AuditModelBase, IProductionDependent
    {
        [DisplayName("Идентификатор типа ближайших объектов к пожару")]
        [Key]
        public Guid ImportantObjectTypeId { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }

        [DisplayName("Сокращенное название")]
        public string? ShortName { get; set; }

        [DisplayName("Основной тип")]
        public bool BasicType { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}
