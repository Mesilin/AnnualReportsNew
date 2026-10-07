using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Aviales
{

    [Table("CitizenAppeal", Schema = "Aviales")]
    [DisplayName("Обращение граждан")]
    [Serializable]
	[AssociativeToString("Обращение от {CitizenAppealDate}")]
	public partial class CitizenAppeal : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid CitizenAppealId { get; set; }

        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Идентификатор типа обращения")]
        public Guid CitizenAppealKindId { get; set; }

        [DisplayName("Дата обращения")]
        public DateTime CitizenAppealDate { get; set; }

        [DisplayName("Контактная информация")]
        [MaxLength(1500)]
		public string? Contact { get; set; }
	
        [Required]
        [DisplayName("Описание")]
        [MaxLength(2000)]
		public string? Description { get; set; }

        [DisplayName("Местоположение")]
        [MaxLength(1500)]
		public string? Location { get; set; }

        [DisplayName("Принятые меры")]
        [MaxLength(1500)]
		public string? Measure { get; set; }

        [DisplayName("Результат")]
        [MaxLength(4000)]
		public string? Result { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Кто принял")]
        public string? Receiver { get; set; }

        [DisplayName("Номер обращения")]
        public short? Number { get; set; }

        [ForeignKey("CitizenAppealKindId")]
        [DisplayName("Тип обращения")]
        public virtual CitizenAppealKind CitizenAppealKind { get; set; }

        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString()
        {
            return "Обращение от " + CitizenAppealDate.ToShortDateString();
        }
    }
}