using Aviales.Model.Flight;
using Incom.Common2.Attributes;
using Incom.Production.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
    [Table("Fire", Schema = "ForestFire")]
    [DisplayName("Лесной пожар")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Лесной пожар \u2116 {NumberForestry}, {ForestryName} л-во, {Year} г.")]
	public class Fire : FireParameters
    {
        [Key]
        [DisplayName("Идентификатор пожара")]
        public Guid FireId { get; set; }

        [DisplayName("Список выполненных работ во время налета")]
        public virtual ICollection<FireFlightWork> FireFlightWorks { get; set; } = new List<FireFlightWork>();

        [DisplayName("Список динамик")]
        [Aggregation]
        public virtual ICollection<FireDynamic> FireDynamics { get; set; } = new List<FireDynamic>();

        [DisplayName("Список ближайших к пожару объектов")]
        [Aggregation]
        public virtual ICollection<NearestObject> NearestObjects { get; set; } = new List<NearestObject>();

        [DisplayName("Затраты и ущерб лесному хозяйству")]
        [Aggregation]
        public virtual ICollection<FireDamage> FireDamages { get; set; } = new List<FireDamage>();

        [DisplayName("Арендаторы")]
        [Aggregation]
        public virtual ICollection<FireLeaseholder> FireLeaseholders { get; set; } = new List<FireLeaseholder>();

		[NotMapped] public FireDynamic? LastDinamika { get; set; }

        [NotMapped] public FireDynamic? FirstDynamic { get; set; }

        [NotMapped] public string? RegionName { get; set; }

        public override string ToAuditString()
        {
            //TODO переделать типы номеров и года в инт. в fireview тоже
            return "Лесной пожар №" + (int)NumberForestry + ", " + ForestryName + " л-во, " + (int)Year + " г.";

        }
    }
}