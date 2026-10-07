using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Aviales;
using Incom.Common2.Attributes;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{

	[Table("FireDynamic", Schema = "ForestFire")]
	[DisplayName("Динамика пожара")]
	[Serializable]
	[AssociativeToString("Динамика от {FireDynamicDate}")]
	public class FireDynamic : FireDynamicParameters
	{
		[Key]
		[DisplayName("Идентификатор динамики")]
		public Guid FireDynamicId { get; set; }

		[DisplayName("Пожар")]
		public Guid FireId { get; set; }
		
		[DisplayName("Список техники")]
		[Aggregation]
		public virtual ObservableCollection<FightFireResource> FightFireResources { get; set; } = new();

		[DisplayName("Список людей")]
		[Aggregation]
		public virtual ObservableCollection<FightFireTeam> FightFireTeams { get; set; } = new();

		[DisplayName("Список кварталов")]
		[Aggregation]
		public virtual ObservableCollection<FireDynamicQuarter> FireDynamicQuarters { get; set; } = new();

		[DisplayName("Список привлечённых ВС")]
		[Aggregation]
		public virtual ICollection<AttractedAircraft> AttractedAircrafts { get; set; }

		[ForeignKey("FireId")]
		public virtual Fire? Fire { get; set; }

		[NotMapped]
		public string? PeopleList { get; set; }
		[NotMapped]
		public string? TechnicList { get; set; }

		/// <summary>
		/// Признак КЧС. Для пожаров до 24 года - это динамика с FireDynamicStateKind.Code == 8. 
		/// После 24 года это динамика с NotLandingReasonKind.Code == 1 или последняя динамика с ликвидацией и любым NotLandingReasonKind, если предпоследняя динамика IsCes
		/// </summary>
		[DisplayName("Признак КЧС")]
		public bool IsCes { get; set; }

		public override string ToAuditString()
		{
			return $"Динамика от {FireDynamicDate:dd.MM.yyyy HH:mm}";
		}
	}
}