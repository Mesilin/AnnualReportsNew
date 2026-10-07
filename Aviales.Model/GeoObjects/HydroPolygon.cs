/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: HydroPolygon.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.09.2019
*  Назначение: Определение класса HydroPolygon
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.GeoObjects
{
    /// <summary>
    /// Объект карты "Водный полигон"
    /// </summary>
    [DisplayName("Водный полигональный объект карты")]
    [Table("HydroPolygon", Schema = "GeoObjects")]
    public class HydroPolygon : ModelBase
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid HydroPolygonId { get; set; }

        /// <summary>
        /// Идентификатор класса гидро объекта
        /// </summary>
        [DisplayName("Идентификатор класса гидро объекта")]
        public int? HydroPolygonClassId { get; set; }

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
        //public Geometry HydroPolygonGeom { get; set; }

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
