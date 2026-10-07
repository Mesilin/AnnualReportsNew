/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireActExtinguishingMethod.cs
* Автор: Анохин А.Ю.
* Дата создания: 01.12.2021
* Назначение: Определение класса LandscapeFireActExtinguishingMethod
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Interfaces;
using UIP.Core.Model.Configuration;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFireActExtinguishingMethod", Schema = "LandscapeFire")]
    [DisplayName("Способы тушения пожара")]
    [Serializable]
	[AssociativeToString("{Name}")]
	public partial class LandscapeFireActExtinguishingMethod : AuditModelBase, IProductionDependent
    {
        [Key]
        [DisplayName("Идентификатор")]
        public Guid LandscapeFireActExtinguishingMethodId { get; set; }
        
        /// <summary>
        /// Наименование способа тушения пожара
        ///  </summary>
        /// <remarks>Захлестывание, окопка, опашка трактором, заливание водой из ранцевых опрыскивателей, пожарных автоцистерн, с помощью мотопомп, при помощи химрастворов и т.п.</remarks>
        [DisplayName("Наименование способа")]
        [MaxLength(250)]
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        public Guid? ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        [DisplayName("Внедрение")]
        public virtual Production? Production { get; set; }

        public override string ToAuditString() => Name;
    }
}
