using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Navigation
{

    [Table("AbonentGroup", Schema = "Navigation")]
    [DisplayName("Группа абонентов")]
	[AssociativeToString("{cName}")]
	public partial class AbonentGroup : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор группы абонентов")]
        public Guid AbonentGroupId { get; set; }

        [MaxLength(50)]
        public string? cName { get; set; }

        public bool? bMoving { get; set; }
        //public byte[] imgImage { get; set; }
        public Guid? AbonentGroupImageId { get; set; }
        public Guid? ParentId { get; set; }
        public string? RegionCode { get; set; }

        [DisplayName("Список абонентов")]
        [XmlIgnore]
        public virtual ICollection<Abonent> Abonents { get; set; } = new List<Abonent>();
        [XmlIgnore]
        public virtual ICollection<AbonentGroup> AbonentGroup1 { get; set; } = new List<AbonentGroup>();

        [ForeignKey("ParentId")]
        public virtual AbonentGroup? AbonentGroup2 { get; set; }
        [ForeignKey("AbonentGroupImageId")]
        public virtual AbonentGroupImage? AbonentGroupImage { get; set; }
        public override string ToAuditString()
        {
            return cName;
        }
    }
}