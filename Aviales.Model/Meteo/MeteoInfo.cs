using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;

namespace Aviales.Model.Meteo
{

	[Table("MeteoInfo", Schema = "Meteo")]
	[DisplayName("Метеоинформация")]
	[Serializable]
	public partial class MeteoInfo : ModelBase, IProductionDependent
	{
		[Key]
		public Guid MeteoInfoId {get; set;}
        public Guid MeteostationId {get; set;}
		public DateTime MeteoInfoDate {get; set;}
		public int? KppoNesterov {get; set;}
		public byte? KpoNesterov {get; set;}
		public int? KppoPv1 {get; set;}
		public byte? KpoPv1 {get; set;}
		public int? KppoPv2 {get; set;}
		public byte? KpoPv2 {get; set;}
		public decimal? Precipitation {get; set;}
		public decimal? Temperature {get; set;}
		public decimal? DewPoint {get; set;}
		public byte? WindAvg {get; set;}
		public bool IsForecast {get; set;}
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

		[ForeignKey("MeteostationId")]
		public virtual Meteostation Meteostation {get; set;}
	}
}