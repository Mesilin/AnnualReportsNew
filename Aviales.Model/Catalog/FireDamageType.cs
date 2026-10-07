/****************************************************************************
*  Copyright (C) 2022 Инком. Все права защищены.
*
*  Файл: FireDamage.cs
*  Автор: Е.В. Месилин
*  Дата создания: 2022.11.22
*  Назначение: Определение класса FireDamageType
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Catalog
{
    [DisplayName("Тип ущерба от пожара")]
    [Table("FireDamageType", Schema = "Catalog")]
    [Serializable]
    public class FireDamageType: ModelBase
    {
        [DisplayName("Идентификатор типа ущерба от пожара")]
        [Key]
        public Guid FireDamageTypeId { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }

        [DisplayName("Порядковый №")]
        public short OrderNum { get; set; }
    }
}
