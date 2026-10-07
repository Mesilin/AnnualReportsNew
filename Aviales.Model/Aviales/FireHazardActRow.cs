using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Aviales.Model.Oktmo;
using Incom.Common.Collections;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{

    [Table("FireHazardActRow", Schema = "Aviales")]
    [DisplayName("Строка акта пожароопасности")]
	[AssociativeToString("Строка акта пожароопасности от {Date}")]
	public partial class FireHazardActRow : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FireHazardActRowId { get; set; }

        [DisplayName("Идентификатор акта")]
        public Guid FireHazardActId { get; set; }

        [DisplayName("Идентификатор мун. р-на")]
        public Guid? MunicipalDistrictId { get; set; }

        [DisplayName("Идентификатор вида режима")]
        public Guid FireHazardKindId { get; set; }

        /// <summary>
        /// ActType: 0 - Введение, 1 - Снятие
        /// </summary>
        [DisplayName("Тип акта")]
        public ActivityType ActivityType { get; set; }

        [Required]
        [Column("DateOfEntryIntoForce")]
        [DisplayName("Дата вступления в силу")]
        public DateTime Date { get; set; }

        [DisplayName("Запрет посещения лесов")]
        public bool IsForestVisitForbidden { get; set; }

        [ForeignKey("FireHazardActId")]
        [DisplayName("Акт")]
        public virtual FireHazardAct FireHazardAct { get; set; }

        [ForeignKey("MunicipalDistrictId")]
        [DisplayName("Муниципальный район")]
        public virtual SettlementOktmo? MunicipalDistrict { get; set; }

        [ForeignKey("FireHazardKindId")]
        [DisplayName("Вид режима")]
        public virtual FireHazardKind FireHazardKind { get; set; }

        public override string ToAuditString()
        {
            return typeof(ActivityType).GetDisplayName(ActivityType) + " режима c " + Date.ToShortDateString();
        }
    }


    /// <summary>
    /// Тип угроз
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// Введение
        /// </summary>
        [EnumDisplayName("Введение")]
        Enter = 0,
        /// <summary>
        /// Снятие
        /// </summary>
        [EnumDisplayName("Снятие")]
        Removal = 1
    }
}