/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: NearestObject.cs
*  Автор: Данилов А.В.
*  Дата создания: 06.09.2019
*  Назначение: Определение класса NearestObject
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
    /// <summary>
    /// Ближайшие объекты к пожару (нефтегазовые объекты, дороги, водные... )
    /// </summary>
    [Table("NearestObjects", Schema = "ForestFire")]
    [DisplayName("Ближайшие объекты к пожару")]
    [Serializable]
	[AssociativeToString("Ближайший объект {ImportantObjectType}")]
	public class NearestObject : NearestObjectParameters
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid NearestObjectId { get; set; }

        /// <summary>
        /// Идентификатор пожара
        /// </summary>
        [DisplayName("Идентификатор пожара")]
        public Guid FireId { get; set; }

        /// <summary>
        /// Объект пожара
        /// </summary>
        [ForeignKey("FireId")]
        public virtual Fire Fire { get; set; }
    }
}
