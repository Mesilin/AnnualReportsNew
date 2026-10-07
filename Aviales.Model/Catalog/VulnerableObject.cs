using Incom.Common2.Persistence;
using Incom.Production.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
	[Table("VulnerableObject", Schema = "Catalog")]
	[Description("Реестр уязвимых объектов. При пожаре в радиусе мониторинга происходит рассылка уведомлений")]
	[DisplayName("Реестр уязвимых объектов")]
	[AssociativeToString("{Name}")]
	public class VulnerableObject : AuditModelBase, IProductionDependent
	{
		[Key]
		public Guid VulnerableObjectId { get; set; }

		[DisplayName("Наименование")]
		[MaxLength(250)]
		public string? Name { get; set; }

		[DisplayName("Описание")]
		[MaxLength(1500)]
		public string? Description { get; set; }

		[DisplayName("Широта")]
		public double? Latitude { get; set; }

		[DisplayName("Долгота")]
		public double? Longitude { get; set; }

		[DisplayName("Радиус мониторинга, километров")]
		public int? ObservedRadius { get; set; }

		public Guid? ProductionId { get; set; }

		public override string ToAuditString()
		{
			return Name;
		}
	}
}
