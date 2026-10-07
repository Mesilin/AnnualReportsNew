using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Meteo
{

	[Table("MeteostationRegion", Schema = "Meteo")]
	[DisplayName("Связка метеостанция cубъект")]
	[Serializable]
	[AssociativeToString("Связка cубъекта: {Region} и метеостанции: {Meteostation}")]
	public partial class MeteostationRegion : AuditModelBase
	{
		[Key]
		public Guid MeteostationRegionId {get; set;}

		public Guid RegionId {get; set;}
		public Guid MeteostationId {get; set;}

		[ForeignKey("RegionId")]
		public virtual Region Region {get; set;} = null!;

		[ForeignKey("MeteostationId")]
		public virtual Meteostation Meteostation {get; set;} = null!;
	}
}