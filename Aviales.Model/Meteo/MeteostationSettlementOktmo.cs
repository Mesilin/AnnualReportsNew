using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Interfaces;
using Aviales.Model.Oktmo;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Meteo
{
	[Table("MeteostationSettlementOktmo", Schema = "Meteo")]
	[DisplayName("Связка метеостанция - районы")]
    [Serializable]
	[AssociativeToString("Связка района: {SettlementOktmo} и метеостанции: {Meteostation}")]
	public partial class MeteostationSettlementOktmo : AuditModelBase, IMeteostationLinkObject, IProductionDependent
{
		[Key]
        [DisplayName("Идентификатор")]
		public Guid MeteostationSettlementOktmoId { get; set; }

        [DisplayName("Идентификатор района")]
		public Guid SettlementOktmoId { get; set; }

        [DisplayName("Идентификатор метеостанции")]
		public Guid MeteostationId { get; set; }

        [DisplayName("Вес метеостанции в районе")]
		public decimal? Weight { get; set; }

		[ForeignKey("SettlementOktmoId")]
		[DisplayName("Муниципальный район")]
		public virtual SettlementOktmo SettlementOktmo { get; set; } = null!;

		[ForeignKey("MeteostationId")]
		[DisplayName("Метеостанция")]
		public virtual Meteostation Meteostation { get; set; } = null!;

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }
}
}