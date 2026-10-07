using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Flight
{

    [Table("PersonnelDynamic", Schema = "Aviales")]
    [DisplayName("Динамика личного состава")]
    [Serializable]
	[AssociativeToString("Динамика Л/С от {PersonnelDynamicDate}")]
	public partial class PersonnelDynamic : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid PersonnelDynamicId { get; set; }

        [DisplayName("Идентификатор сотрудника")]
        public Guid PersonnelId { get; set; }

        [DisplayName("Дата динамики")]
        public DateTime PersonnelDynamicDate { get; set; }

        [DisplayName("Идентификатор авиаотделения")]
        public Guid? AirbaseDepartmentId { get; set; }

        [DisplayName("Должность")]
        public Guid? WorkPositionId { get; set; }

        [MaxLength(6)]
        public string? Code { get; set; }

        [ForeignKey("AirbaseDepartmentId")]
        [DisplayName("Авиаотделение")]
        public virtual AirbaseDepartment? AirbaseDepartment { get; set; }

        [ForeignKey("PersonnelId")]
        [DisplayName("Сотрудник")]
        public virtual Personnel Personnel { get; set; } = null!;

        [ForeignKey("WorkPositionId")]
        [DisplayName("Должность")]
        public virtual WorkPosition? WorkPosition { get; set; }

        [NotMapped]
        public string? AirbaseDepartmentName
        {
            get;
            set;
        }

        public override string ToAuditString() => "Динамика личного состава от " + PersonnelDynamicDate.ToShortDateString();
    }
}