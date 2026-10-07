using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Interfaces;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Meteo
{

    [Table("MeteostationForestry", Schema = "Meteo")]
    [DisplayName("Связка метеостанция лесничество")]
    [Serializable]
	[AssociativeToString("Лес-во {Forestry}, метеостанция {Meteostation}")]
	public partial class MeteostationForestry : AuditModelBase, IMeteostationLinkObject
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid MeteostationForestryId { get; set; }

        [DisplayName("Идентификатор лесничества")]
        public Guid ForestryId { get; set; }

        [DisplayName("Идентификатор метеостанции")]
        public Guid MeteostationId { get; set; }
        
        [DisplayName("Вес метеостанции в лесничестве")]
        public decimal? Weight { get; set; }

        [DisplayName("Лесничество")]
        [ForeignKey("ForestryId")]
        public virtual Forestry Forestry { get; set; }

        [DisplayName("Метеостанция")]
        [ForeignKey("MeteostationId")]
        public virtual Meteostation Meteostation { get; set; }
    }
}