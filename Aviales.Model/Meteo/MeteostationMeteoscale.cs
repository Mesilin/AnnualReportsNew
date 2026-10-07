using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Meteo
{
    [Table("MeteostationMeteoscale", Schema = "Meteo")]
	[DisplayName("Связка метеошкала метеостанция")]
	[Serializable]
    [AssociativeToString("Связка шкалы: {Meteoscale} и метеостанции: {Meteostation}")]
	public partial class MeteostationMeteoscale : AuditModelBase
	{
		[Key]
        [DisplayName("Идентификатор")]
        public Guid MeteostationMeteoscaleId {get; set;}

        [DisplayName("Идентификатор метеошкалы")]
		public Guid MeteoscaleId {get; set; }
        [DisplayName("Идентификатор метеостанции")]
        public Guid MeteostationId {get; set; }

        [ForeignKey("MeteoscaleId")]
        [DisplayName("Метеошкала")]
        public virtual Meteoscale Meteoscale { get; set; } = null!;

        [ForeignKey("MeteostationId")]
        [DisplayName("Метеостанция")]
        public virtual Meteostation Meteostation { get; set; } = null!;
	}
}