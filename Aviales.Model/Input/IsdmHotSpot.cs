using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Input
{
    [Table("IsdmHotSpot", Schema = "Input")]
	[DisplayName("ИСДМ-точка")]
	[Serializable]
	public class IsdmHotSpot : ModelBase, IProductionDependent
	{
		[Key]
		public Guid IsdmHotSpotId {get; set;}

		[Required]
		[MaxLength(100)]
		public string? Number {get; set;}

        public int Season { get; set; }

        public double Latitude {get; set;}
		public double Longitude {get; set;}
		public string? City {get; set;}
		public string? ForestryIsdmCode {get; set;}
		public decimal? Azimuth {get; set;}
		public decimal? Distance {get; set;}
		public DateTime FirstFixationDate {get; set;}
		public DateTime? LastFixationDate {get; set;}
		public decimal FixationArea {get; set;}
		public bool? StateOnDate {get; set;}
		public DateTime? LiquidationDate {get; set;}
		public decimal AreaAll {get; set;}
		public decimal? AreaPokr {get; set;}
		public string? Comment {get; set;}

        /// <summary>
        /// Этот номер уникален только в рамках сезона. Смена сезона в ИСДМ происходит в первой половине января.
        /// </summary>
        public string? IsdmSeasonFireId { get; set; }

		
        public byte[] Wkb {get; set;}
		public decimal? AreaTotal {get; set;}
		public decimal? AreaLesTotal {get; set;}
		public decimal? AreaLesFondTotal {get; set;}
		public decimal? AreaLesFondLesTotal {get; set;}
		public decimal? AreaLesFond {get; set;}
		public decimal? AreaLesFondLes {get; set;}
		public string? ForestryName {get; set;}
		public string? ForestryDistrictName {get; set;}
		public string? Quarter {get; set;}
        /// <summary>
        /// Урочище
        /// </summary>
        public string? ForestryTractName { get; set; }
		public Guid? ForestryId {get; set;}
		public Guid RegionId {get; set;}
		public Guid? IsdmHotSpotExclusionReasonKindId {get; set;}
		public Guid? IsdmHotSpotZoneKindId {get; set;}
		public Guid? IsdmHotSpotStateKindIdOnDate {get; set;}
		public Guid IsdmHotSpotStateKindId {get; set;}
		public string? ValidationResult { get; set; }
        public bool? IsConfirmed { get; set; }
        public string? ConfirmedBy { get; set; }
        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

		/// <summary>
		/// Если state==2, тут будет IsdmSeasonFireId с которым объединился пожар
		/// </summary>
		public string? MergedToFireWithIsdmSeasonFireId { get; set; }

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;}

		[ForeignKey("ForestryId")]
		public virtual Forestry? Forestry { get; set; }

		[ForeignKey("IsdmHotSpotExclusionReasonKindId")]
		public virtual IsdmHotSpotExclusionReasonKind? IsdmHotSpotExclusionReasonKind {get; set;}

		[ForeignKey("IsdmHotSpotStateKindId")]
		public virtual IsdmHotSpotStateKind IsdmHotSpotStateKind {get; set;}

		[ForeignKey("IsdmHotSpotStateKindIdOnDate")]
		public virtual IsdmHotSpotStateKind? IsdmHotSpotStateKindOnDate {get; set;}

		[ForeignKey("IsdmHotSpotZoneKindId")]
		public virtual IsdmHotSpotZoneKind? IsdmHotSpotZoneKind {get; set;}
		
        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }
	}
}
