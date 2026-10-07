using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
	[Table("FireAnalytics", Schema = "ForestFire")]
	[DisplayName("Пожарная аналитика")]
	[Serializable]
	[AssociativeToString("Пожарная аналитика от {AnalyticsDate}")]
	public partial class FireAnalytics : AuditModelBase
	{
		[Key]
		public Guid FireAnalyticsId {get; set;}

		public Guid RegionId {get; set;}
		public DateTime AnalyticsDate {get; set;}
		public int? FireCountOccurredToday {get; set;}
		public int? BigFireCountOccurredToday {get; set;}
		public int? FireCountDetectedAndLiquidatedToday {get; set;}
		public decimal? CoverAreaToday {get; set;}
		public decimal? NoncoverAreaToday {get; set;}
		public decimal? CrownAreaToday {get; set;}
		public decimal? NonforestAreaToday {get; set;}

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;}
	}
}