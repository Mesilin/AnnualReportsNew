using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Navigation
{
    [Table("Abonent", Schema = "Navigation")]
    [DisplayName("Абонент")]
	[AssociativeToString("{Name}")]
	public partial class Abonent : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор абонента")]
        public Guid AbonentId { get; set; }

        [DisplayName("Номер")]
        [Required]
        [MaxLength(5)]
        public string? Number { get; set; }

        public Guid? AbonentGroupId { get; set; }

        [DisplayName("Наименование абонента")]
        [MaxLength(200)]
        public string? Name { get; set; }

        [DisplayName("Идентификатор родительского абонента")]
        public Guid? ParentId { get; set; }

        [XmlIgnore]
        [DisplayName("Список дочерних абонентов")]
        public virtual ICollection<Abonent> ChildAbonents { get; set; } = new List<Abonent>();

        [DisplayName("Родительский абонент")]
        [ForeignKey("ParentId")]
        public virtual Abonent? Parent { get; set; }

        [DisplayName("Группа")]
        [ForeignKey("AbonentGroupId")]
        public virtual AbonentGroup? AbonentGroup { get; set; }

        [XmlIgnore]
        [Aggregation]
        public virtual ICollection<Track>? Tracks { get; set; }

        [XmlIgnore]
        [DisplayName("Коллекция GPS точек")]
        public virtual ICollection<GpsPoint>? GpsPoints { get; set; }

        [XmlIgnore]
        [DisplayName("Каналы")]
        [Aggregation]
        public virtual ICollection<AbonentChannel>? AbonentChannels { get; set; }

        public override string ToAuditString()
        {
            return Name;
        }
    }
}