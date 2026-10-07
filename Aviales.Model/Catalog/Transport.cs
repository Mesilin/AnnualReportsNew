/****************************************************************************
*  Copyright (C) 2018 Инком. Все права защищены.
*
*  Файл: Transport.cs
*  Автор: Истомин М.О.
*  Дата создания: 1/30/2018 5:24:18 PM
*  Назначение: Определение класса Transport
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{
	[Table("Transport", Schema = "Catalog")]
	[DisplayName("Транспорт")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public class Transport : AuditModelBase, IProductionDependent
	{
		[Key]
		[DisplayName("Идентификатор транспорта")]
		public Guid TransportId { get; set; }

        [DisplayName("Тип")]
		public Guid DeliveryTransportTypeId { get; set; }

		[Required]
		[DisplayName("Наименование транспорта")]
		public string? Name { get; set; }

		[Required]
		[MaxLength(30)]
		[DisplayName("Сокращенное наименование транспорта")]
		public string? ShortName { get; set; }

		[DisplayName("Признак активности")]
		public bool IsActive { get; set; }

		[ForeignKey("DeliveryTransportTypeId")]
		public virtual DeliveryTransportType DeliveryTransportType { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Внедрение")]
		public Guid? ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}
