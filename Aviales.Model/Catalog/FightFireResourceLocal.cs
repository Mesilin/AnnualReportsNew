using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

    [Table("FightFireResourceLocal", Schema = "Catalog")]
    [DisplayName("Региональные справочники. Типы средств тушения")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class FightFireResourceLocal : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid FightFireResourceLocalId { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Год")]
        public int Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Является технической оснасткой
        /// </summary>
        [DisplayName("Является технической оснасткой")]
        public bool IsTechnicalEquipment { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        [DisplayName("Идентификатор типа сил и средств пожаротушения")]
        public Guid? FightFireForceKindId { get; set; }

        [ForeignKey("FightFireForceKindId")]
        [DisplayName("Тип сил и средств пожаротушения")]
        public virtual FightFireForceKind? FightFireForceKind { get; set; }
        
        public override string ToAuditString()
        {
            return Name;
        }
    }
}