using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Meteo
{

	[Table("Meteoscale", Schema = "Meteo")]
	[DisplayName("Метеошкала")]
	[Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Метеошкала с {BeginDate} по {EndDate}")]
	public partial class Meteoscale : AuditModelBase, IProductionDependent
	{
        [Key]
        [DisplayName("Идентификатор")]
		public Guid MeteoscaleId {get; set;}

        [DisplayName("Дата начала действия метеошкалы")]
		public DateTime BeginDate {get; set; }

        [DisplayName("Дата окончания действия метеошкалы")]
        public DateTime EndDate {get; set; }

        [DisplayName("Верхняя граница комплексного показателя метеоусловий, в котором КПО=1")]
        public short? LimitKpo1 {get; set; }

        [DisplayName("Верхняя граница комплексного показателя метеоусловий, в котором КПО=2")]
        public short? LimitKpo2 {get; set; }

        [DisplayName("Верхняя граница комплексного показателя метеоусловий, в котором КПО=3")]
        public short? LimitKpo3 {get; set; }
        
        [DisplayName("Верхняя граница комплексного показателя метеоусловий, в котором КПО=4")]
        public short? LimitKpo4 {get; set;}

		[Aggregation(true)]
        [DisplayName("Метеостанции")]
		public virtual ICollection<MeteostationMeteoscale> MeteostationMeteoscales {get; set;} = new List<MeteostationMeteoscale>();

        [DisplayName("Идентификатор внедрения")]
		public Guid? ProductionId { get; set; }
        
        [DisplayName("Внедрение")]
        [ForeignKey("ProductionId")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() =>
            "Метеошкала с " + BeginDate.ToShortDateString() + " по " + EndDate.ToShortDateString();
    }
}