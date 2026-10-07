using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

	[Table("FireHazard", Schema = "Aviales")]
	[DisplayName("Пожароопасность")]
	[Serializable]
	[AssociativeToString("Запись акта от {FireHazardDate}")]
	public partial class FireHazard : AuditModelBase
	{
        [Key]
		public Guid FireHazardId {get; set;}

		public Guid? FireHazardKindId {get; set;}
		public Guid RegionId {get; set;}
		public DateTime FireHazardDate {get; set;}
		public DateTime? EnablingDate {get; set;}

		[MaxLength(2000)]
		public string? EnablingDocument {get; set;}

		public DateTime? DisablingDate {get; set;}

		[MaxLength(2000)]
		public string? DisablingDocument {get; set;}

		public bool IsAllTerritory {get; set;}
		public string? Description {get; set;}
		public int? RegionKpo {get; set;}
		public int? RegionDeadCount {get; set;}
		public int? RegionVictimCount {get; set;}
		public int? RegionDestructionCount {get; set;}
		public bool IsConfirmed {get; set;}

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;}

		[ForeignKey("FireHazardKindId")]
		public virtual FireHazardKind? FireHazardKind {get; set;}

		public virtual ICollection<FireHazardConsequence> FireHazardConsequences {get; set;} = new List<FireHazardConsequence>();
        public virtual ICollection<FireHazardDistrict> FireHazardDistricts {get; set; } = new List<FireHazardDistrict>();
    }
}