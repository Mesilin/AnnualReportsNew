using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("NotLandingReasonKind", Schema = "Catalog")]
    [DisplayName("Федеральные справочники. Причины непринятия мер")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class NotLandingReasonKind : AuditModelBase
	{
        [Key]
		public Guid NotLandingReasonKindId {get; set;}

		public int Code {get; set;}

		[Required]
		public string? Name {get; set;}

		public string? ShortName {get; set;}
		public int? Year {get; set;}
		public bool IsActive {get; set;}
		
        public override string ToAuditString()
        {
            return ShortName;
        }
    }
}