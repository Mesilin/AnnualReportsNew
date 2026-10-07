using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Interfaces;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Meteo
{

	[Table("MeteostationAirbaseDepartment", Schema = "Meteo")]
	[DisplayName("Связка метеостанция авиаотделение")]
	[Serializable]
	[AssociativeToString("Авиабаза {AirbaseDepartment}, метеостанция {Meteostation}")]
	public partial class MeteostationAirbaseDepartment : AuditModelBase, IMeteostationLinkObject
	{
		[Key]
        [DisplayName("Идентификатор")]
		public Guid MeteostationAirbaseDepartmentId { get; set;}

        [DisplayName("Идентификатор метеостанции")]
		public Guid MeteostationId { get; set;}

        [DisplayName("Идентификатор авиаотделения")]
		public Guid AirbaseDepartmentId { get; set;}

        [DisplayName("Веc метеостанции в авиаотделении")]
		public decimal? Weight { get; set;}

		[ForeignKey("AirbaseDepartmentId")]
        [DisplayName("Авиаотделение")]
		public virtual AirbaseDepartment AirbaseDepartment {get; set;}

		[ForeignKey("MeteostationId")]
        [DisplayName("Метеостанция")]
		public virtual Meteostation Meteostation {get; set;}
	}
}