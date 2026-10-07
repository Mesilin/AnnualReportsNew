using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.ForestFire;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("FireLeaseholder", Schema = "Catalog")]
	[DisplayName("Связка Пожар-Арендаторы")]
	[Serializable]
	[AssociativeToString("{Leaseholder} на пожаре {Fire}")]
	public partial class FireLeaseholder : AuditModelBase, IProductionDependent
	{
        [Key]
		[DisplayName("Идентификатор")]
		public Guid FireLeaseholderId { get; set;}

		[DisplayName("Идентификатор пожара")]
        public Guid FireId { get; set; }

		[DisplayName("Идентификатор арендатора")]
        public Guid LeaseholderId { get; set; }

		[DisplayName("Пожар")]
		[ForeignKey("FireId")]
		public virtual Fire Fire { get; set; }

		[DisplayName("Арендатор")]
		[ForeignKey("LeaseholderId")]
		public virtual Leaseholder Leaseholder { get; set; }

		[DisplayName("Идентификатор внедрения")]
		public Guid? ProductionId { get; set; }

	}
}