using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.Flight
{

	[Table("Personnel", Schema = "Flight")]
	[DisplayName("Справочники налёта воздушных судов. Личный состав")]
	[Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("{Name}")]
	public partial class Personnel : AuditModelBase, IProductionDependent
	{
        [Key]
        [DisplayName("Идентификатор")]
		public Guid PersonnelId { get; set;}

		[MaxLength(30)]
        [DisplayName("Табельный №")]
		public string? Number { get; set;}

		[Required]
        [DisplayName("ФИО")]
		public string? Name {get; set;}

        public string? RegionCode { get; set; }
	    
        [DisplayName("Активность")]
        public bool IsActive {get; set;}

	    [DisplayName("Состав экипажей в налетах")]
		public virtual ICollection<FireFlightPersonnel> FireFlightPersonnels {get; set;} = new List<FireFlightPersonnel>();

        [DisplayName("Список динамик")]
		public virtual ICollection<PersonnelDynamic> PersonnelDynamics {get; set;} = new List<PersonnelDynamic>();

        [NotMapped]
		
	    [DisplayName("Последняя динамика")]
	    [Aggregation]
		public PersonnelDynamic LastPersonnelDynamic
		{
			get
            {
                return PersonnelDynamics
                    .Any(w => !w.IsDeleted)
                    ? PersonnelDynamics
                        .Where(w => !w.IsDeleted)
                        .OrderByDescending(o => o.PersonnelDynamicDate)
                        .First()
                    : null;
            }
		}

        public Guid? ProductionId { get; set; }

        [ForeignKey("ProductionId")]
	    [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}
