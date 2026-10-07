using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Navigation
{

	[Table("AbonentSyncInfo", Schema = "Navigation")]
	[DisplayName("Информация о синхронизации абонента")]
	public partial class AbonentSyncInfo : AuditModelBase
	{
		[Key]
		public Guid AbonentSyncInfoId {get; set;}

		public Guid AbonentId {get; set;}

		[Required]
		[MaxLength(200)]
		public string? TableName {get; set;}

		[MaxLength(200)]
		public string? SectionName {get; set;}

		public DateTime? LastSyncDate {get; set;}

		[ForeignKey("AbonentId")]
		public virtual Abonent Abonent {get; set;}
	}
}