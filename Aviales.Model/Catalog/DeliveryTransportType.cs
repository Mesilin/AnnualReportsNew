/****************************************************************************
*  Copyright (C) 2018 Инком. Все права защищены.
*
*  Файл: DeliveryTransportType.cs
*  Автор: Картавцева С.А.
*  Дата создания: 03/05/2018 10:50:00 PM
*  Назначение: Определение класса DeliveryTransportType
****************************************************************************/

using Incom.Common2.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{
    [Table("DeliveryTransportType", Schema = "Catalog")]
    [DisplayName("Вид транспорта доставки")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public class DeliveryTransportType : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid DeliveryTransportTypeId { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [DisplayName("Код")]
        public int Code { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}
