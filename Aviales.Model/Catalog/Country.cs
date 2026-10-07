using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
	[Table("Country", Schema = "Catalog")]
	[DisplayName("Страна")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class Country : AuditModelBase
	{
        [Key]
        [DisplayName("Идентификатор")]
		public Guid CountryId { get; set; }

        [Required]
		[DisplayName("Код")]
		public string? Code { get; set; }

        [DisplayName("Наименование")]
		public string? Name {get; set; }

        [DisplayName("Год")]
		public int Year {get; set;}

        public override string ToAuditString()
        {
            return Name;
        }
    }
}