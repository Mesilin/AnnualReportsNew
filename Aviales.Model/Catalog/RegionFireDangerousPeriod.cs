using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Incom.Common2.Persistence;
using System.ComponentModel.DataAnnotations;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using Incom.Production.Attributes;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Catalog
{
	[Table("FireDangerousPeriod", Schema = "Catalog")]
	[DisplayName("Пожароопасный сезон")]
	[Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
    [AssociativeToString("Пажороопасный сезон в {Production} с {FireDangerousPeriodBegin} по {FireDangerousPeriodEnd}")]
	public class FireDangerousPeriod: AuditModelBase, IProductionDependent
	{
		[Key]
		[DisplayName("Идентификатор")]
		public Guid FireDangerousPeriodId { get; set; }
		[DisplayName("Дата начала пожароопасного сезона")]
		public DateTime FireDangerousPeriodBegin { get; set; }
		[DisplayName("Дата окончания пожароопасного сезона")]
		public DateTime? FireDangerousPeriodEnd { get; set; }
		public Guid? ProductionId { get; set; }
        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }
    }
}
