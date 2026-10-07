/****************************************************************************
*  Copyright (C) 2017 Инком. Все права защищены.
*
*  Файл: ZoneDependencies.cs
*  Автор: Истомин М.О.
*  Дата создания: 4/13/2017 5:09:59 PM
*  Назначение: Определение класса ZoneDependencies
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Catalog
{
	[Table("ZoneDependencies", Schema = "Catalog")]
	[DisplayName("Федеральные справочники. Зависимости между зонами мониторинга, принадлежностями лесов и районами применения сил и средств")]
	[Serializable]
	[AssociativeToString("Зависимость принадлежности: {ForestOwnerKind}")]
	public class ZoneDependencies : AuditModelBase
	{
		[Key]
		public Guid ZoneDependenciesId { get; set; }

		public Guid ForestOwnerKindId { get; set; }

		public Guid? FightFireZoneId { get; set; }

		public Guid? MonitoringZoneKindId { get; set; }

		public decimal? BigFireSquare { get; set; }

		public int Year { get; set; }

		[ForeignKey("ForestOwnerKindId")]
		public virtual ForestOwnerKind ForestOwnerKind { get; set; } = null!;

		[ForeignKey("FightFireZoneId")]
		public virtual FightFireZone? FightFireZone { get; set; }

		[ForeignKey("MonitoringZoneKindId")]
		public virtual MonitoringZoneKind? MonitoringZoneKind { get; set; }
	}
}
