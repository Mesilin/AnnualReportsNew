using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;

namespace Aviales.Model.Meteo
{

	[Table("MeteoInfoHourly", Schema = "Meteo")]
	[DisplayName("Часовая метеоинформация")]
	[Serializable]
	public partial class MeteoInfoHourly : ModelBase, IProductionDependent
	{
		[Key]
		public Guid MeteoInfoHourlyId {get; set;}

		public Guid MeteostationId {get; set;}
		public DateTime UtcDate {get; set;}
		public DateTime LocalDate {get; set;}
		public decimal? Temperature {get; set;}
		public bool? TemperatureMeasured {get; set;}
		public decimal? DewPoint {get; set;}
		public bool? DewPointMeasured {get; set;}
		public bool? IsForecast {get; set;}
		public bool? IsPrecipitation {get; set;}
		public bool? IsSmoke {get; set;}
		public decimal? Visibility {get; set;}
		public int? WindDirection {get; set;}
		public int? WindSpeed {get; set;}
		public int? WindGust {get; set;}
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