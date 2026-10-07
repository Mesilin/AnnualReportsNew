using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Oktmo
{

	[Table("SettlementOktmo", Schema = "Oktmo")]
	[DisplayName("Населённый пункт")]
	[Serializable]
	[AssociativeToString("{Name}")]
	public partial class SettlementOktmo : AuditModelBase
	{
		[Key]
		[DisplayName("Идентификатор")]
		public Guid SettlementOktmoId {get; set;}

		[Required]
		[MaxLength(14)]
		public string? Code {get; set;}

		[Required]
		[MaxLength(11)]
		public string? Code2 {get; set;}

		[Required]
		[MaxLength(2)]
		public string? SubCode1 {get; set;}

		[Required]
		[MaxLength(3)]
		public string? SubCode2 {get; set;}

		[Required]
		[MaxLength(3)]
		public string? SubCode3 {get; set;}

		[MaxLength(3)]
		public string? SubCode4 {get; set;}

		[Required]
		[MaxLength(1)]
		public string? P1 {get; set;}

		[Required]
		[MaxLength(1)]
		public string? P2 {get; set;}

		[Required]
		[MaxLength(1)]
		public string? Kch {get; set;}

		[Required]
		[MaxLength(500)]
		[DisplayName("Наименование")]
		public string? Name {get; set;}

		[MaxLength(500)]
		[DisplayName("Наименование2")]
		public string? Name2 {get; set;}

		[MaxLength(100)]
		[DisplayName("Сокр. наименование")]
		public string? ShortName {get; set;}

		[MaxLength(500)]
		[DisplayName("Примечание")]
		public string? Notes {get; set;}

		public Guid? FederalDistrictId {get; set;}
		public Guid? MunicipalDistrictId {get; set;}

		[Required]
		[MaxLength(100)]
		[DisplayName("Федеральный округ")]
		public string? FederalDistrictName {get; set;}

		public Guid? RegionId {get; set;}

		[Required]
		[MaxLength(50)]
		[DisplayName("Субъект")]
		public string? RegionName {get; set;}

		public Guid? SettlementKindId {get; set;}

		[MaxLength(100)]
		[DisplayName("Тип")]
		public string? SettlementKindName {get; set;}

		[DisplayName("Широта")]
		public decimal? Latitude {get; set;}
		[DisplayName("Долгота")]
		public decimal? Longitude {get; set;}

		[MaxLength(11)]
		[DisplayName("Код ОКАТО")]
		public string? Okato {get; set;}

		[DisplayName("Год справочника")]
		public int Year {get; set;}
		[DisplayName("Активность")]
		public bool IsActive {get; set; }

		/// <summary>
		/// Поле введено для того, чтобы разные регионы могли по разному определять - что выводить в перечень административных районов.
		/// К примеру хмао и екатеринбург хотят видеть там помимо муниципальных районов еще и городские округа и прочие муниципальные образования,
		/// поэтому для них в БД сделаем set IsCustomMunicipalUnit=true where "SubCode4" ='   ' and "SubCode2" not like '%00' and "SubCode3" not in('100','400','700','701');
		/// </summary>
		[DisplayName("Является муниципальным образованием")]
		public bool? IsCustomMunicipalUnit { get; set;}

		[ForeignKey("FederalDistrictId")]
		[DisplayName("Федеральный округ")]
		public virtual FederalDistrict? FederalDistrict {get; set;}

		[ForeignKey("MunicipalDistrictId")]
		[DisplayName("Муниципальный район")]
		public virtual SettlementOktmo? MunicipalDistrict {get; set;}

		[ForeignKey("RegionId")]
		[DisplayName("Субъект")]
		public virtual Region? Region {get; set;}

		[ForeignKey("SettlementKindId")]
		[DisplayName("Тип")]
		public virtual SettlementKind? SettlementKind {get; set;}

        public override string ToAuditString() => Name;
    }
}