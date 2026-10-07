using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
	[Table("Region", Schema = "Catalog")]
	[DisplayName("Субъект")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class Region : AuditModelBase
	{
		[Key]
		[DisplayName("Идентификатор")]
		public Guid RegionId { get; set; }

		[Required]
		[MaxLength(5)]
		[IsBasic]
		[DisplayName("Код")]
		public string? Code
		{
			get
			{
				if (code == null)
					return null;
				return code.Trim();
			}
			set
			{
				if (!Equals(code, value))
					code = value;
			}
		}

		private string code;

		[Required]
		[DisplayName("Наименование субъекта")]
		[IsBasic]
		public string? Name { get; set; }

		[DisplayName("Год")]
		[IsBasic]
		public int Year { get; set; }
		[DisplayName("Площадь земель лесного фонда")]
		public decimal? ForestFondSquare { get; set; }

		[IsBasic]
		[DisplayName("Активность")]
		public bool IsActive { get; set; }
		[DisplayName("Идентификатор федерального округа")]
		public Guid? FederalDistrictId { get; set; }

		[DisplayName("Порядок сортировки")]
		public decimal? Sorting { get; set; }

		[MaxLength(8)]
		[DisplayName("ОКТМО")]
		public string? Oktmo { get; set; }

		[DisplayName("Площадь зоны ЛАР")]
		public decimal? AirMonitoringZoneArea { get; set; }
		[DisplayName("Площадь наземной зоны мониторинга")]
		public decimal? GroundMonitoringZoneArea { get; set; }
		[DisplayName("Площадь зоны мониторинга К1")]
		public decimal? Space1MonitoringZoneArea { get; set; }
		[DisplayName("Площадь зоны мониторинга К2")]
		public decimal? Space2MonitoringZoneArea { get; set; }

		[ForeignKey("FederalDistrictId")]
		[DisplayName("Федеральный округ")]
		public virtual FederalDistrict? FederalDistrict { get; set; }

		public override string ToAuditString() => Name;
	}
}