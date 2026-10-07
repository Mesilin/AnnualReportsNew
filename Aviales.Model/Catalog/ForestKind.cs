using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Interfaces;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{

	[Table("ForestKind", Schema = "Catalog")]
	[DisplayName("Характеристики местности и пожара. Целевое назначение лесов")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class ForestKind : AuditModelBase, ITaxationType
	{
		[Key]
        [DisplayName("Идентификатор")]
		public Guid ForestKindId {get; set; }

        [DisplayName("Код")]
        public int Code {get; set; }

        [Required]
        [DisplayName("Наименование")]
		public string? Name {get; set; }

        [DisplayName("Краткое наименование")]
        public string? ShortName {get; set; }

        [DisplayName("Год справочника")]
        public int? Year {get; set; }

        [DisplayName("Активность")]
        public bool IsActive {get; set; }
        
        public override string ToAuditString()
        {
            return Name;
        }
}
}