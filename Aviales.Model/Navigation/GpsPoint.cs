using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Database.Annotations;
using Incom.Common2.Persistence;
using Newtonsoft.Json;

namespace Aviales.Model.Navigation
{

	[Table("GpsPoint", Schema = "Navigation")]
    [DisplayName("GPS точка")]
	[Serializable]
	public class GpsPoint : ModelBase
	{
		[Key]
		public Guid GpsPointId {get; set;}

        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return new DateTime(dateTimeTicks);
            }
            set
            {
                dateTimeTicks = value.Ticks;
            }
        }
        [JsonProperty]
        private long dateTimeTicks;

        [HasPrecision(9, 7)]
        public decimal Latitude {get; set;}
        [HasPrecision(10, 7)]
        public decimal Longitude {get; set;}
		public decimal? Direction {get; set;}
		public decimal? Speed {get; set;}
		public short? Satellite {get; set;}
		public bool IsActual {get; set;}
		public DateTime DateTimeEdit {get; set;}
		public decimal Mileage {get; set;}
		public decimal? MotoHour {get; set;}

		public string? Events {get; set;}

		public bool? HasAlert {get; set;}
		public short? Altitude {get; set;}
		public decimal? VoltAccum {get; set;}
		public decimal? VoltBort {get; set;}

        public Guid AbonentId { get; set; }

        [ForeignKey("AbonentId")]
        public virtual Abonent Abonent { get; set; }
	}
}