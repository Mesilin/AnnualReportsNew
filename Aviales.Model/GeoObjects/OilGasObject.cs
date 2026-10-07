/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: OilGasObject.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.09.2019
*  Назначение: Определение класса OilGasObject
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.GeoObjects
{
    /// <summary>
    /// Объект карты "Объект нефтегазовой промышленности"
    /// </summary>
    [DisplayName("Объект нефтегазовой промышленности")]
    [Table("OilGasObjects", Schema = "GeoObjects")]
    public class OilGasObject : ModelBase
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid OilGasObjectsId { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [DisplayName("Широта")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [DisplayName("Долгота")]
        public double? Longitude { get; set; }

        ///// <summary>
        ///// Геометрия объекта карты
        ///// </summary>
        //[DisplayName("Геометрия объекта карты")]
        //public Geometry OilGasObjectsGeom { get; set; }

        /// <summary>
        /// Геометрия объекта карты в формате WKT
        /// </summary>
        [DisplayName("Геометрия объекта карты в формате WKT")]
        public string Wkt { get; set; }
    }
}
