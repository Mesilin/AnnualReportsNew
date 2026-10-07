/****************************************************************************
*  Copyright (C) 2022 Инком. Все права защищены.
*
*  Файл: FireDamage.cs
*  Автор: Е.В. Месилин
*  Дата создания: 2022.11.22
*  Назначение: Определение класса FireDamage
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.ForestFire
{
    [Table("FireDamage", Schema = "ForestFire")]
    [DisplayName("Затраты и ущерб лесному хозяйству")]
    [Serializable]
	[AssociativeToString("Ущерб {FireDamageType}")]
	public class FireDamage : FireDamageParameters
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid FireDamageId { get; set; }

        /// <summary>
        /// Идентификатор пожара
        /// </summary>
        [DisplayName("Идентификатор пожара")]
        public Guid FireId { get; set; }

        /// <summary>
        /// Объект пожара
        /// </summary>
        [ForeignKey("FireId")]
        public virtual Fire? Fire { get; set; }
    }
}
