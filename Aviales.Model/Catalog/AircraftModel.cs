using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Flight;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.Catalog
{
    [Table("AircraftModel", Schema = "Catalog")]
    [DisplayName("Модель воздушного судна")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
    [AssociativeToString("{Name}")]
	public partial class AircraftModel : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid AircraftModelId { get; set; }

        /// <summary>
        /// Идентификатор родительского ВС
        /// </summary>
        [DisplayName("Идентификатор родительского ВС")]
        public Guid? ParentAircraftModelId { get; set; }

        [DisplayName("Идентификатор типа ВС")]
        public Guid AircraftKindId { get; set; }

        [Required]
        [DisplayName("Наименование")]
        public string? Name { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [DisplayName("Класс")]
        public byte? Class { get; set; }

        [DisplayName("Кр. скорость")]
        public decimal? Speed { get; set; }

        [DisplayName("Расход топлива")]
        public decimal? FuelConsumption { get; set; }

        [DisplayName("Запас топлива")]
        public decimal? FuelCapacity { get; set; }

        [DisplayName("Длина ВПП")]
        public decimal? AirstripLenght { get; set; }

        [DisplayName("Аэронавигационный запас")]
        public byte? AeroTimeShift { get; set; }

        [DisplayName("Год справочника")]
        public int? Year { get; set; }

        [DisplayName("Активность")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Номер группы судов по форме 2-2-Авиа.
        /// </summary>
        [Range(0, 8)]
        [DisplayName("Номер группы судов по форме 2-2-Авиа")]
        public byte? GroupNumber { get; set; }

        /// <summary>
        /// ВС из федерального справочника?
        /// </summary>
        [DisplayName("ВС из федерального справочника?")]
        public bool IsFederal { get; set; }

        [ForeignKey("AircraftKindId")]
        [DisplayName("Тип воздушного судна")]

        public virtual AircraftKind AircraftKind { get; set; } = null!;

        [ForeignKey("ParentAircraftModelId")]
        [DisplayName("Базовая модель")]
        public virtual AircraftModel ParentAircraftModel { get; set; } = null!;

        [DisplayName("Ставки оплаты летной работы")]
        [Aggregation]
        public virtual ICollection<FireFlightPaymentRate> FireFlightPaymentRates { get; set; } = null!;

        public Guid? ProductionId { get; set; }
        public override string ToAuditString()
        {
            return Name;
        }
    }
}