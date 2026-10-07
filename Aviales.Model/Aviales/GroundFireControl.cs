using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.Aviales
{

	[Table("GroundFireControl", Schema = "Aviales")]
	[DisplayName("Контроль наземного пожара")]
	[Serializable]
	public partial class GroundFireControl : AuditModelBase
	{
		[Key]
		public Guid GroundFireControlId {get; set;}

		public Guid RegionId {get; set;}
		public int Year {get; set;}
		public string? Institution {get; set;}
		public decimal? Square {get; set;}
		public string? SquareNote {get; set;}
		public int? GroundTransportCount {get; set;}
		public string? GroundTransportCountNote {get; set;}
		public int? GroundTransportMinuteCount {get; set;}
		public string? GroundTransportMinuteCountNote {get; set;}
		public int? DetectedFireCount {get; set;}
		public string? DetectedFireCountNote {get; set;}
		public int? LiquidatedFireCount {get; set;}
		public string? LiquidatedFireCountNote {get; set;}
		public int? OutsideLiquidatedFireCount {get; set;}
		public string? OutsideLiquidatedFireCountNote {get; set;}
		public decimal? OutsideLiquidatedFireSquare {get; set;}
		public string? OutsideLiquidatedFireSquareNote {get; set;}
		public int? SelfLiquidatedFireCount {get; set;}
		public string? SelfLiquidatedFireCountNote {get; set;}
		public decimal? SelfLiquidatedFireSquare {get; set;}
		public string? SelfLiquidatedFireSquareNote {get; set;}
		public decimal? LiquidatedFireSquare {get; set;}
		public string? LiquidatedFireSquareNote {get; set;}
		public int? WaterTransportCount {get; set;}
		public string? WaterTransportCountNote {get; set;}
		public int? WaterTransportMinuteCount {get; set;}
		public string? WaterTransportMinuteCountNote {get; set;}
		public int? Work {get; set;}
		public string? WorkNote {get; set;}
		public bool IsConfirmed {get; set;}

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;}
	}
}