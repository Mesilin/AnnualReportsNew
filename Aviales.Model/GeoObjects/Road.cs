/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: Road.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.09.2019
*  Назначение: Определение класса Road
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.GeoObjects
{
    /// <summary>
    /// Объект карты "Дорога"
    /// </summary>
    [DisplayName("Объект карты - дорога")]
    [Table("Roads", Schema = "GeoObjects")]
    public class Road : ModelBase
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid RoadsId { get; set; }

        /// <summary>
        /// Идентификатор класса объекта дороги
        /// </summary>
        [DisplayName("Идентификатор класса объекта дороги")]
        public int? RoadsClassId { get; set; }

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

        ///// <summary>
        ///// Геометрия объекта карты
        ///// </summary>
        //[DisplayName("Геометрия объекта карты")]
        //public Geometry RoadsGeom { get; set; }

        /// <summary>
        /// Геометрия объекта карты в формате WKT
        /// </summary>
        [DisplayName("Геометрия объекта карты в формате WKT")]
        public string Wkt { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }
    }
}
