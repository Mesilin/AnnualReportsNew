/****************************************************************************
*  Copyright (C) 2021 Инком. Все права защищены.
*
*  Файл: CondensateLine.cs
*  Автор: Волегова Л.В.
*  Дата создания: 21.04.2021
*  Назначение: Определение класса CondensateLine
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.GeoObjects
{
    /// <summary>
    /// Объект карты "Конденсатопроводы"
    /// </summary>
    [DisplayName("Объект карты - Конденсатопроводы")]
    [Table("CondensateLine", Schema = "GeoObjects")]
    public class CondensateLine : ModelBase
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid CondensateLineId { get; set; }

       
        /// <summary>
        /// Широта крайней северной точки
        /// </summary>
        [DisplayName("Широта крайней северной точки")]
        public double? Latitude1 { get; set; }

        /// <summary>
        /// Долгота крайней западной точки
        /// </summary>
        [DisplayName("Долгота крайней западной точки")]
        public double? Longitude1 { get; set; }

        /// <summary>
        /// Широта крайней южной точки
        /// </summary>
        [DisplayName("Широта крайней южной точки")]
        public double? Latitude2 { get; set; }

        /// <summary>
        /// Долгота крайней восточной точки
        /// </summary>
        [DisplayName("Долгота крайней восточной точки")]
        public double? Longitude2 { get; set; }
        
        /// <summary>
        /// Геометрия объекта карты в формате WKT
        /// </summary>
        [DisplayName("Геометрия объекта карты в формате WKT")]
        public string? wkt { get; set; }
    }
}
