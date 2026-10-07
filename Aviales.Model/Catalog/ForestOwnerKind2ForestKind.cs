/****************************************************************************
*  Copyright (C) 2017 Инком. Все права защищены.
*
*  Файл: ForestOwnerKind2ForestKind.cs
*  Автор: Истомин М.О.
*  Дата создания: 4/13/2017 5:35:01 PM
*  Назначение: Определение класса ForestOwnerKind2ForestKind
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Incom.Common2.Persistence;
using Newtonsoft.Json;

namespace Aviales.Model.Catalog
{
	[Table("ForestOwnerKind2ForestKind", Schema = "Catalog")]
	[DisplayName("Связка принадлежности земель и целевое назначение лесов")]
	[Serializable]
	public class ForestOwnerKind2ForestKind : ModelBase
	{
		[Key]
		public Guid ForestOwnerKind2ForestKindId { get; set; }

		public Guid ForestOwnerKindId { get; set; }

		public Guid ForestKindId { get; set; }

		public int? Year { get; set; }

		[ForeignKey("ForestOwnerKindId")]
		public virtual ForestOwnerKind ForestOwnerKind { get; set; }

		[ForeignKey("ForestKindId")]
		public virtual ForestKind ForestKind { get; set; }

		/// <summary>
		/// Код принадлежности, не маппится, может задаваться независимо от данных, нужно для сопоставления со старыми данными из ясень-ф
		/// </summary>
		/// <value>The forest owner kind code not mapped.</value>
		[NotMapped]
		[XmlIgnore]
		[JsonIgnore]
		public decimal? ForestOwnerKindCodeNotMapped
		{
			get
			{
				if (ForestOwnerKind != null && forestOwnerCode==null)
				{
					forestOwnerCode = ForestOwnerKind.Code;
				}
				return forestOwnerCode;
			}
			set
			{
				forestOwnerCode = value;
			}
		}
		[NonSerialized]
		private decimal? forestOwnerCode;

		/// <summary>
		/// Код типа леса, не маппится, может задаваться независимо от данных, нужно для сопоставления со старыми данными из ясень-ф
		/// </summary>
		/// <value>The forest kind code not mapped.</value>
		[NotMapped]
		[XmlIgnore]
		[JsonIgnore]
		public decimal? ForestKindCodeNotMapped
		{
			get
			{
				if (ForestKind != null && forestCode == null)
				{
					forestCode = ForestKind.Code;
				}
				return forestCode;
			}
			set
			{
				forestCode = value;
			}
		}
		[NonSerialized]
		private decimal? forestCode;
	}
}
