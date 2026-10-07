using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Flight
{

    [Table("FireFlightPaymentRate", Schema = "Flight")]
    [DisplayName("Ставки оплаты летной работы")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Ставки оплаты работы ВС: {AircraftModel}")]
	public partial class FireFlightPaymentRate : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireFlightPaymentRateId { get; set; }
        
        [DisplayName("Дата, с которой вступает в действие")]
        public DateTime RateStartDate { get; set; }

        [DisplayName("Ставка 1")]
        public decimal? Rate1 { get; set; }

        [DisplayName("Ставка 2")]
        public decimal? Rate2 { get; set; }

        [DisplayName("Ставка 3")]
        public decimal? Rate3 { get; set; }

        [DisplayName("Ставка 4")]
        public decimal? Rate4 { get; set; }

        [DisplayName("Ставка 5")]
        public decimal? Rate5 { get; set; }

        [DisplayName("Ставка 6")]
        public decimal? Rate6 { get; set; }

        [DisplayName("Идентификатор модели ВС")]
        public Guid AircraftModelId { get; set; }

        [ForeignKey("AircraftModelId")]
        [DisplayName("Модель ВС")]
        public virtual AircraftModel AircraftModel { get; set; } = null!;

		public Guid? ProductionId { get; set; }
        public override string ToAuditString()
        {
            return "Ставки оплаты ";
        }
    }
}