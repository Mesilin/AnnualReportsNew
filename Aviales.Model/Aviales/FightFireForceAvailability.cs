using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	[Table("FightFireForceAvailability", Schema = "Aviales")]
	[DisplayName("Наличие сил и средств тушения")]
	[AssociativeToString("аличие сил и средств тушения от {Month} {Year}")]
	[Serializable]
	public partial class FightFireForceAvailability : AuditModelBase
	{
		[Key]
		public Guid FightFireForceAvailabilityId {get; set;}

		public Guid RegionId {get; set;}
		public int Month {get; set;}
		public int Year {get; set;}
		public int? TractorAndBulldozerCount {get; set;}
		public int? FireEngineCount {get; set;}
		public int? FightLeaderCount {get; set;}
		public int? ObserverPilotCount {get; set;}
		public int? ParachutistCount {get; set;}
		public int? ParatrooperCount {get; set;}
		public int? GroundWorkerCount {get; set;}
		public int? OutsideTractorAndBulldozerCount {get; set;}
		public int? OutsideFireEngineCount {get; set;}
		public int? OutsideFightFireWorkerCount {get; set;}
		public int? AircraftCount {get; set;}

		[ForeignKey("RegionId")] public virtual Region Region { get; set; } = null!;
	}
}