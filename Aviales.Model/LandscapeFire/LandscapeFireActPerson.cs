/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireActPerson.cs
* Автор: Анохин А.Ю.
* Дата создания: 23.10.2023
* Назначение: Определение класса LandscapeFireActPerson
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Enums;
using Incom.Common2.Persistence;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFireActPerson", Schema = "LandscapeFire")]
    [DisplayName("Связка. Акта о ландшафтном пожаре и должностного лица")]
    [Serializable]
    public partial class LandscapeFireActPerson : ModelBase
    {
        /// <summary>
        /// Идентификатор связки
        /// </summary>
        [Key]
        [DisplayName("Идентификатор связки")]
        public Guid LandscapeFireActPersonId { get; set; }

        /// <summary>
        /// Идентификатор акта о природном пожаре
        /// </summary>
        [DisplayName("Идентификатор акта о природном пожаре")]
        public Guid LandscapeFireActId { get; set; }

        /// <summary>
        /// Акт о ланшафтном пожаре
        /// </summary>
        [ForeignKey("LandscapeFireActId")]
        public virtual LandscapeFireAct? LandscapeFireAct { get; set; }

        /// <summary>
        /// Уникальный идентификатор должностного лица
        /// </summary>
        [DisplayName("Уникальный идентификатор должностного лица")]
        public Guid PersonId { get; set; }
        
        /// <summary>
        /// Типы должностного лица
        /// </summary>
        [DisplayName("Типы должностного лица")]
        public LandscapeFireActPersonType LandscapeFireActPersonType { get; set; }
    }
}
