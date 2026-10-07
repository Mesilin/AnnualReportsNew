/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: GasFlare.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.09.2019
*  Назначение: Определение класса GasFlare
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.GeoObjects
{
    /// <summary>
    /// Объект карты "Газовый факел"
    /// </summary>
    [DisplayName("Газовый факел")]
    [Table("GasFlares", Schema = "GeoObjects")]
    public class GasFlare : ModelBase
    {
        /// <summary>
        /// Ключ-идентификатор
        /// </summary>
        [DisplayName("Ключ-идентификатор")]
        [Key]
        public Guid GasFlaresId { get; set; }

        public string? deposit { get; set; }

        public string? model { get; set; }

        public string? state { get; set; }

        public decimal diametr { get; set; }

        public decimal number { get; set; }

        public string? name { get; set; }

        public string? flowmeter { get; set; }

        public string? Owner { get; set; }

        ///// <summary>
        ///// Номер станции
        ///// </summary>
        //[DisplayName("Номер станции")]
        //public int? NumberOfStation { get; set; }

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

        /// <summary>
        /// Геометрия объекта карты
        /// </summary>
        //[DisplayName("Геометрия объекта карты")]
        //public Geometry GasFlaresGeom { get; set; }

        public string? geom { get; set; }

        /// <summary>
        /// Геометрия объекта карты в формате WKT
        /// </summary>
        [DisplayName("Геометрия объекта карты в формате WKT")]
        public string? wkt { get; set; }
    }
}
